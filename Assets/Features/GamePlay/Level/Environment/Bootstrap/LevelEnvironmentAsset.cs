using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Level.Environment.Bootstrap
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "Environment",
        menuName = GamePlayAssetsPaths.Environment + "Service")]
    public class LevelEnvironmentAsset : LocalServiceAsset
    {
        [SerializeField] private AssetReference _scene;

        public override async UniTask Create(IServiceBinder serviceBinder, ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var data = new TypedSceneLoadData<ILevelBootstrapper>(_scene);
            var bootstrapper = await sceneLoader.Load(data);
            callbacksRegister.ListenContainerCallbacks((MonoBehaviour)bootstrapper.Searched);
        }
    }
}