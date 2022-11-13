using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace GamePlay.Services.Network.Bootstrap.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.NetworkPrefix + "LocalNetwork",
        menuName = GamePlayAssetsPaths.NetworkBootstrapper + "Service")]
    public class NetworkSessionAsset : LocalServiceAsset
    {
        [SerializeField] private AssetReference _scene;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var sceneLoadResult = await sceneLoader.Load(new TypedSceneLoadData<INetworkSessionBootstrapper>(_scene));
            SceneManager.SetActiveScene(sceneLoadResult.Instance.Scene);
            
            var bootstrapper = sceneLoadResult.Searched;
            bootstrapper.Bootstrap(serviceBinder, callbacksRegister);

            callbacksRegister.ListenLoopCallbacks(bootstrapper);
        }
    }
}