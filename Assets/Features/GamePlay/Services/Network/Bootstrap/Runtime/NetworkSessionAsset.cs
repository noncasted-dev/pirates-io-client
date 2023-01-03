using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace GamePlay.Services.Network.Bootstrap.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.NetworkPrefix + "LocalNetwork",
        menuName = GamePlayAssetsPaths.NetworkBootstrapper + "Service")]
    public class NetworkSessionAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private AssetReference _scene;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var sceneLoadResult = await sceneLoader.Load(new TypedSceneLoadData<INetworkSessionBootstrapper>(_scene));
            SceneManager.SetActiveScene(sceneLoadResult.Instance.Scene);

            var bootstrapper = sceneLoadResult.Searched;
            
            bootstrapper.Bootstrap(builder);

            callbacks.Listen(bootstrapper);
        }
    }
}