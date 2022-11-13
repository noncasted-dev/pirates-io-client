using GamePlay.Player.Entity.Views.Transforms.Runtime;
using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Weapons.Cannon.Views.Transforms
{
    [DisallowMultipleComponent]
    public class CannonTransform : TransformView, ICannonTransform
    {
        [Inject]
        private void Construct(ILogger logger)
        {
            CreateLogger(logger);
        }
    }
}