using GamePlay.Player.Entity.Views.Transforms.Runtime;
using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Weapons.Bow.Views.Transforms
{
    [DisallowMultipleComponent]
    public class BowTransform : TransformView, IBowTransform
    {
        [Inject]
        private void Construct(ILogger logger)
        {
            CreateLogger(logger);
        }
    }
}