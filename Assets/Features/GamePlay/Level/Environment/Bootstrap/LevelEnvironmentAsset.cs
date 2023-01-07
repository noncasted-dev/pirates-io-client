using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Level.Environment.Bootstrap
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "Environment",
        menuName = GamePlayAssetsPaths.Environment + "Service")]
    public class LevelEnvironmentAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private AssetReference _scene;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var data = new TypedSceneLoadData<ILevelBootstrapper>(_scene);
            var bootstrapper = await sceneLoader.Load(data);
            callbacks.Listen((MonoBehaviour)bootstrapper.Searched);
        }
    }
}