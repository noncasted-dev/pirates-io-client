using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Views.Transforms.Runtime
{
    public class PlayerBodyTransform : TransformView, IBodyTransform
    {
        [Inject]
        private void Construct(ILogger logger)
        {
            CreateLogger(logger);
        }

        public GameObject GameObject => gameObject;
    }
}