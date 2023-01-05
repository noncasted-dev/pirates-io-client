using Cysharp.Threading.Tasks;
using Global.GameLoops.Runtime;
using Global.Services.Common.Config.Abstract;
using Global.Services.Common.Scope;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using ContainerBuilder = Common.DiContainer.Runtime.ContainerBuilder;

namespace Global.Bootstrappers
{
    [DisallowMultipleComponent]
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GlobalScope _scope;
        [SerializeField] private AssetReference _servicesScene;

        [SerializeField] private GameLoopAsset _gameLoop;
        [SerializeField] private GlobalServicesConfig _services;

        private void Awake()
        {
            Bootstrap().Forget();
        }

        private async UniTaskVoid Bootstrap()
        {
            var scene = await Addressables.LoadSceneAsync(_servicesScene, LoadSceneMode.Additive).ToUniTask();

            var binder = new GlobalServiceBinder(scene.Scene);
            var sceneLoader = new GlobalSceneLoader();
            var callbacks = new GlobalCallbacks();
            var dependencyRegister = new ContainerBuilder();

            binder.AddToModules(_scope);

            var services = _services.GetAssets();
            var servicesTasks = new UniTask[services.Length];

            var gameLoop = _gameLoop.Create(dependencyRegister, binder);

            for (var i = 0; i < servicesTasks.Length; i++)
                servicesTasks[i] = services[i].Create(dependencyRegister, binder, sceneLoader, callbacks);

            await UniTask.WhenAll(servicesTasks);

            using (LifetimeScope.Enqueue(OnConfiguration))
            {
                await UniTask.Create(async () => _scope.Build());
            }

            void OnConfiguration(IContainerBuilder builder)
            {
                dependencyRegister.RegisterAll(builder);
            }

            dependencyRegister.ResolveAllWithCallbacks(_scope.Container, callbacks);

            await callbacks.InvokeFlowCallbacks();

            gameLoop.OnAwake();
            gameLoop.Begin();
        }
    }
}