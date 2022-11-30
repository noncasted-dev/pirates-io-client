using Common.EditableScriptableObjects.Attributes;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.LevelLoops.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.LevelLoops.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "LevelLoop",
        menuName = GamePlayAssetsPaths.LevelLoop + "Service")]
    public class LevelLoopAsset : LocalServiceAsset
    {
        [SerializeField]  private LevelLoopLogSettings _logSettings;
        [SerializeField] private LevelLoop _prefab;

        public override async UniTask Create(IServiceBinder serviceBinder, ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var levelLoop = Instantiate(_prefab);
            levelLoop.name = "LevelLoop";

            serviceBinder.Register<LevelLoopLogger>()
                .WithParameter(_logSettings);

            serviceBinder.RegisterComponent(levelLoop)
                .AsSelf();

            serviceBinder.AddToModules(levelLoop);
            callbacksRegister.ListenLoopCallbacks(levelLoop);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            resolver.Resolve<LevelLoop>();
        }
    }
}