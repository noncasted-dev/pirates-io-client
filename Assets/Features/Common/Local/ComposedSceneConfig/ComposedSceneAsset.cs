using System;
using System.Collections.Generic;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;
using ContainerBuilder = Common.DiContainer.Runtime.ContainerBuilder;

namespace Common.Local.ComposedSceneConfig
{
    public abstract class ComposedSceneAsset : ScriptableObject
    {
        [SerializeField] private ComposedScenesConfig _config;

        public async UniTask<ComposedSceneLoadResult> Load(LifetimeScope parent, ISceneLoader loader)
        {
            var scenes = AssignScenes();
            var services = AssignServices();

            var sceneLoader = new ComposedSceneLoader(loader);
            var servicesTasks = new List<UniTask>();
            var scenesTasks = new List<UniTask>();

            var loadServicesScene = await sceneLoader.Load(new EmptySceneLoadData(_config.ServicesScene));
            var servicesScene = loadServicesScene.Instance.Scene;
            var transformer = new LocalServiceTransformer(servicesScene);

            var serviceBinder = new LocalServiceBinder(transformer);
            var selfCallbacks = new LocalCallbacks();
            var builder = new ContainerBuilder();

            foreach (var scene in scenes)
            {
                var task = sceneLoader.Load(new EmptySceneLoadData(scene));
                scenesTasks.Add(task);
            }

            foreach (var service in services)
            {
                var task = service.Create(builder, serviceBinder, loader, selfCallbacks);
                servicesTasks.Add(task);
            }

            await UniTask.WhenAll(scenesTasks);
            await UniTask.WhenAll(servicesTasks);

            var scopePrefab = AssignScope();
            var scope = Instantiate(scopePrefab);
            serviceBinder.AddToModules(scope);

            selfCallbacks.InvokeRegisterCallbacks(builder);

            using (LifetimeScope.EnqueueParent(parent))
            {
                using (LifetimeScope.Enqueue(Register))
                {
                    await UniTask.Create(async () => scope.Build());
                }
            }

            void Register(IContainerBuilder container)
            {
                builder.RegisterAll(container);
            }

            builder.ResolveAllWithCallbacks(scope.Container, selfCallbacks);

            selfCallbacks.Resolve(scope.Container);
            selfCallbacks.InvokeAwakeCallbacks();
            selfCallbacks.InvokeEnableCallback();

            await selfCallbacks.InvokeAsyncAwakeCallbacks();

            return new ComposedSceneLoadResult(
                sceneLoader.Results,
                selfCallbacks.SwitchCallbacks,
                scope,
                selfCallbacks.InvokeLoadedCallbacks);
        }

        protected virtual AssetReference[] AssignScenes()
        {
            return Array.Empty<AssetReference>();
        }

        protected virtual LocalServiceAsset[] AssignServices()
        {
            return Array.Empty<LocalServiceAsset>();
        }

        protected abstract LifetimeScope AssignScope();
    }
}