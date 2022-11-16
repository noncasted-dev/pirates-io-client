#region

using Cysharp.Threading.Tasks;
using Global.GameLoops.Runtime;
using Global.Services.Common.Config.Abstract;
using Global.Services.Common.Scope;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

#endregion

namespace Global.Bootstrappers
{
    [DisallowMultipleComponent]
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GlobalScope _scope;
        [SerializeField] private GameLoopAsset _gameLoop;
        [SerializeField] private AssetReference _servicesScene;
        [SerializeField] private GameObject _camera;

        [SerializeField] private GlobalServicesConfig _services;

        private void Awake()
        {
            Bootstrap().Forget();
        }

        private async UniTaskVoid Bootstrap()
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

            Destroy(_camera);

            binder.InvokeFlowCallbacks();

            IObjectResolverExtensions.Resolve<GameLoop>(_scope.Container).Begin();
        }
    }
}