using Common.DiContainer.Abstract;
using Common.EditableScriptableObjects.Attributes;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.LevelLoops.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.LevelLoops.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "LevelLoop",
        menuName = GamePlayAssetsPaths.LevelLoop + "Service")]
    public class LevelLoopAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private LevelLoopLogSettings _logSettings;
        [SerializeField] [Indent] private LevelLoop _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var levelLoop = Instantiate(_prefab);
            levelLoop.name = "LevelLoop";

            builder.Register<LevelLoopLogger>()
                .WithParameter(_logSettings);

            builder.RegisterComponent(levelLoop)
                .AsCallbackListener();

            serviceBinder.AddToModules(levelLoop);
        }
    }
}