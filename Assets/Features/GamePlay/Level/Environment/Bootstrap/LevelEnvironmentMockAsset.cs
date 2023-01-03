using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Level.Environment.Bootstrap
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "EnvironmentMock",
        menuName = GamePlayAssetsPaths.Environment + "Mock")]
    public class LevelEnvironmentMockAsset : LevelEnvironmentAsset
    {
        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var bootstrapper = FindObjectOfType<EnvironmentBootstrapper>();
            builder.RegisterComponent(bootstrapper)
                .AsCallbackListener();
        }
    }
}