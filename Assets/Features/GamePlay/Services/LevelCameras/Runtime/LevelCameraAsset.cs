using Common.EditableScriptableObjects.Attributes;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.LevelCameras.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;

namespace GamePlay.Services.LevelCameras.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "LevelCamera",
        menuName = GamePlayAssetsPaths.LevelCamera + "Service")]
    public class LevelCameraAsset : LocalServiceAsset
    {
        [SerializeField]  private LevelCameraConfigAsset _config;
        [SerializeField]  private LevelCameraLogSettings _logSettings;
        [SerializeField] private LevelCamera _prefab;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var levelCamera = Instantiate(_prefab);
            levelCamera.name = "LevelCamera";

            serviceBinder.Register<LevelCameraLogger>()
                .WithParameter(_logSettings)
                .AsSelf();

            serviceBinder.Register<LevelCameraConfig>()
                .WithParameter(_config)
                .As<ILevelCameraConfig>();

            serviceBinder.RegisterComponent(levelCamera)
                .As<ILevelCamera>()
                .AsSelf();

            serviceBinder.AddToModules(levelCamera);
            callbacksRegister.ListenLoopCallbacks(levelCamera);
        }
    }
}