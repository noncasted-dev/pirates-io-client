using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.GameLoops.Logs;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.GameLoops.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "Global",
        menuName = GlobalAssetsPaths.GameLoop + "Service", order = 0)]
    public class GameLoopAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private GameLoopLogSettings _logSettings;
        [SerializeField] private GameLoop _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var gameLoop = Instantiate(_prefab);
            gameLoop.name = "GameLoop";

            builder.Register<GameLoopLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            builder.RegisterComponent(gameLoop).AsImplementedInterfaces();

            serviceBinder.ListenCallbacks(gameLoop);
            serviceBinder.AddToModules(gameLoop);
        }
    }
}