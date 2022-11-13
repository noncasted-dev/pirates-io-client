using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace GamePlay.Services.Network.Bootstrap.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.NetworkPrefix + "LocalNetwork",
        menuName = GamePlayAssetsPaths.NetworkBootstrapper + "Service")]
    public class NetworkSessionAsset : LocalServiceAsset
    {
        [SerializeField] private NetworkSessionBootstrapper _prefab;
        [SerializeField] private AssetReference _scene;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var sceneLoadResult = await sceneLoader.Load(new TypedSceneLoadData<INetworkSessionBootstrapper>(_scene));
            SceneManager.SetActiveScene(sceneLoadResult.Instance.Scene);

            var bootstrapper = Instantiate(_prefab);
            bootstrapper.name = "SessionNetworkBootstrapper";

            serviceBinder.RegisterComponent(bootstrapper);
            callbacksRegister.ListenLoopCallbacks(bootstrapper);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            resolver.Resolve<NetworkSessionBootstrapper>();
        }
    }
}