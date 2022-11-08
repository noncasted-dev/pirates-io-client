using Cysharp.Threading.Tasks;
using Global.GameLoops.Abstract;
using Global.Services.Common.Config.Abstract;
using Global.Services.Common.Scope;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Global.Bootstrappers
{
    [DisallowMultipleComponent]
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GlobalScope _scope;
        [SerializeField] private GlobalGameLoopAsset _gameLoop;
        [SerializeField] private AssetReference _servicesScene;

        [SerializeField] private GlobalServicesConfig _services;

        private void Awake()
        {
            Bootstrap().Forget();
        }

        private async UniTaskVoid Bootstrap()
        {
            var scene = await Addressables.LoadSceneAsync(_servicesScene, LoadSceneMode.Additive).ToUniTask();

            var binder = new ServiceBinder(scene.Scene);

            GlobalGameLoop gameLoop = null;

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

                gameLoop = _gameLoop.Create(builder, binder.AddToModules);
            }

            binder.InvokeFlowCallbacks();

            gameLoop.OnAwake();
            gameLoop.OnBootstrapped();
            gameLoop.Begin();
        }
    }
}