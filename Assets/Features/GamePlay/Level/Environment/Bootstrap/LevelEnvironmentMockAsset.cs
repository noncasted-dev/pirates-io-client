using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;

namespace GamePlay.Level.Environment.Bootstrap
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "EnvironmentMock",
        menuName = GamePlayAssetsPaths.Environment + "Mock")]
    public class LevelEnvironmentMockAsset : LevelEnvironmentAsset
    {
        public override async UniTask Create(IServiceBinder serviceBinder, ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var bootstrapper = FindObjectOfType<EnvironmentBootstrapper>();
            serviceBinder.RegisterComponent(bootstrapper);
            callbacksRegister.ListenContainerCallbacks(bootstrapper);
        }
    }
}