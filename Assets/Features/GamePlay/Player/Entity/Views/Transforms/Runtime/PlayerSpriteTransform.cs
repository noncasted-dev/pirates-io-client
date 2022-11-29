using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Views.Transforms.Runtime
{
    public class PlayerSpriteTransform : TransformView, ISpriteTransform, ISwitchCallbacks
    {
        [Inject]
        public void Construct(ILogger logger, IUpdater updater)
        {
            _updater = updater;

            CreateLogger(logger);
        }

        private Impact _last;

        private IUpdater _updater;

        public void Impact(Vector2 direction, float distance, float time)
        {
            if (_last is { IsEnded: false })
                return;

            _last = new Impact(_updater, this, direction, distance, time);

            _last.Start();
        }

        public void OnDisabled()
        {
            _last?.Stop();
        }
        
        
        public void OnEnabled()
        {
            
        }
    }
}