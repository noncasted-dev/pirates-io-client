using Cysharp.Threading.Tasks;
using GamePlay.Level.Config.Runtime;
using Global.Bootstrappers;
using Global.GameLoops.Runtime;
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

namespace GamePlay.Common.GlobalBootstrapMocks
{
    [DisallowMultipleComponent]
    public class GlobalBootstrapMock : MonoBehaviour
    {
        [SerializeField] private GlobalScope _scope;
        [SerializeField] private GameLoopAsset _gameLoop;
        [SerializeField] private LevelAsset _level;
        [SerializeField] private AssetReference _servicesScene;

        [SerializeField] private GlobalServicesConfig _services;

        private void Awake()
        {
            Process().Forget();
        }

        private async UniTask Process()
        {
            await BootstrapGlobal();
            await BootstrapLocal();
        }

        private async UniTask BootstrapGlobal()
        {
            var scene = await Addressables.LoadSceneAsync(_servicesScene, LoadSceneMode.Additive).ToUniTask();

            var binder = new ServiceBinder(scene.Scene);

            binder.AddToModules(_scope);

            using (LifetimeScope.Enqueue(OnConfiguration))
            {
                await UniTask.Create(async () => _scope.Build());
            }

            void OnConfiguration(IContainerBuilder builder)
            {
                var services = _services.GetAssets();

                foreach (var service in services)
                    service.Create(builder, binder);

                _gameLoop.Create(builder, binder);
            }

            var container = _scope.Container;

            container.Resolve<GameLoop>();

            binder.InvokeFlowCallbacks();

            var connector = container.Resolve<INetworkConnector>();
            var joiner = container.Resolve<INetworkSessionJoiner>();

            var userName = $"Player_{Random.Range(0, 301)}";
            await connector.Connect(userName);
            await joiner.JoinRandom();
        }

        private async UniTask BootstrapLocal()
        {
            var result = await _level.Load(_scope, _scope.Container.Resolve<ISceneLoader>());

            result.OnLoaded();
        }
    }
}