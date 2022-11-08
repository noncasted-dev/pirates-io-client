using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Local.ComposedSceneConfig
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

            var serviceBinder = new ServiceBinder(transformer);
            var callbacksRegister = new CallbacksRegister();

            foreach (var scene in scenes)
            {
                var task = sceneLoader.Load(new EmptySceneLoadData(scene));
                scenesTasks.Add(task);
            }

            foreach (var service in services)
            {
                var task = service.Create(serviceBinder, callbacksRegister, sceneLoader);
                servicesTasks.Add(task);
            }

            await UniTask.WhenAll(scenesTasks);
            await UniTask.WhenAll(servicesTasks);

            var scopePrefab = AssignScope();
            var scope = Instantiate(scopePrefab);
            serviceBinder.AddToModules(scope);

            using (LifetimeScope.EnqueueParent(parent))
            {
                using (LifetimeScope.Enqueue(Register))
                {
                    await UniTask.Create(async () => scope.Build());
                }
            }

            void Register(IContainerBuilder builder)
            {
                serviceBinder.RegisterAllQueued(builder);
                callbacksRegister.InvokeRegisterCallbacks(builder);
            }

            foreach (var service in services)
                service.OnResolve(scope.Container, callbacksRegister);

            callbacksRegister.Resolve(scope.Container);
            callbacksRegister.InvokeAwakeCallbacks();
            callbacksRegister.InvokeEnableCallback();
            await callbacksRegister.InvokeAsyncAwakeCallbacks();

            return new ComposedSceneLoadResult(
                sceneLoader.Results,
                callbacksRegister.SwitchCallbacks,
                scope,
                callbacksRegister.InvokeLoadedCallbacks);
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