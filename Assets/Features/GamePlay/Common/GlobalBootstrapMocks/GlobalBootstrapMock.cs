using Cysharp.Threading.Tasks;
using GamePlay.Level.Config.Runtime;
using Global.Bootstrappers;
using Global.GameLoops.Abstract;
using Global.Services.Common.Config.Abstract;
using Global.Services.Common.Scope;
using Global.Services.Network.Connection.Runtime;
using Global.Services.Network.Session.Join.Runtime;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using ContainerBuilder = Common.DiContainer.Runtime.ContainerBuilder;

namespace GamePlay.Common.GlobalBootstrapMocks
{
    [DisallowMultipleComponent]
    public class GlobalBootstrapMock : MonoBehaviour
    {
        private static bool _isBootstrapped = false;

        [SerializeField] private GlobalScope _scope;
        [SerializeField] private GlobalGameLoopAsset _gameLoop;
        [SerializeField] private LevelAsset _level;
        [SerializeField] private AssetReference _servicesScene;

        [SerializeField] private GlobalServicesConfig _services;

        private void Awake()
        {
            if (_isBootstrapped == true)
                return;

            _isBootstrapped = true;

            Process().Forget();
        }

        private void OnDestroy()
        {
            _isBootstrapped = false;
        }

        private async UniTask Process()
        {
            await BootstrapGlobal();
            await BootstrapLocal();
        }

        private async UniTask BootstrapGlobal()
        {
            var scene = await Addressables.LoadSceneAsync(_servicesScene, LoadSceneMode.Additive).ToUniTask();

            var binder = new GlobalServiceBinder(scene.Scene);
            var sceneLoader = new GlobalSceneLoader();
            var callbacks = new GlobalCallbacks();
            var dependencyRegister = new ContainerBuilder();

            binder.AddToModules(_scope);

            var services = _services.GetAssets();
            var servicesTasks = new UniTask[services.Length];

            _gameLoop.Create(dependencyRegister, binder);

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

            var connector = _scope.Container.Resolve<INetworkConnector>();
            await connector.Connect("Test", TargetServer.USA_SanFrancisco);
            var joiner = _scope.Container.Resolve<INetworkSessionJoiner>();
            await joiner.Create();
        }

        private async UniTask BootstrapLocal()
        {
            var result = await _level.Load(_scope, _scope.Container.Resolve<ISceneLoader>());

            result.OnLoaded();
        }
    }
}