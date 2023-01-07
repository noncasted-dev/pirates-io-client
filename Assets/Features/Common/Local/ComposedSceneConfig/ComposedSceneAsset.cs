using System;
using System.Collections.Generic;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;
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
            var services = AssignServices();

            var sceneLoader = new ComposedSceneLoader(loader);
            var servicesTasks = new List<UniTask>();

            var loadServicesScene = await sceneLoader.Load(new EmptySceneLoadData(_config.ServicesScene));
            var servicesScene = loadServicesScene.Instance.Scene;
            var transformer = new LocalServiceTransformer(servicesScene);

            var serviceBinder = new LocalServiceBinder(transformer);
            var selfCallbacks = new LocalCallbacks();
            var builder = new ContainerBuilder();

            foreach (var service in services)
            {
                var task = service.Create(builder, serviceBinder, sceneLoader, selfCallbacks);
                servicesTasks.Add(task);
            }

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
            await selfCallbacks.InvokeAsyncAwakeCallbacks();

            selfCallbacks.InvokeEnableCallback();

            selfCallbacks.InvokeBootstrapCallbacks();
            await selfCallbacks.InvokeAsyncBootstrapCallbacks();

            return new ComposedSceneLoadResult(
                sceneLoader.Results,
                selfCallbacks.SwitchCallbacks,
                scope,
                selfCallbacks.InvokeLoadedCallbacks);
        }

        protected virtual LocalServiceAsset[] AssignServices()
        {
            return Array.Empty<LocalServiceAsset>();
        }

        protected abstract LifetimeScope AssignScope();
    }
}