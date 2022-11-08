using System;
using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.GameLoops.Abstract;
using Global.GameLoops.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.GameLoops.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "Global",
        menuName = GlobalAssetsPaths.GameLoop + "Service", order = 0)]
    public class GameLoopAsset : GlobalGameLoopAsset
    {
        [SerializeField] [EditableObject] private GameLoopLogSettings _logSettings;
        [SerializeField] private GameLoop _prefab;

        public override GlobalGameLoop Create(IContainerBuilder builder, Action<MonoBehaviour> addToModules)
        {
            var gameLoop = Instantiate(_prefab);
            gameLoop.name = "GameLoop";

            builder.Register<GameLoopLogger>(Lifetime.Scoped)
                .WithParameter("settings", _logSettings);

            builder.RegisterComponent(gameLoop).AsImplementedInterfaces();

            addToModules?.Invoke(gameLoop);

            return gameLoop;
        }
    }
}