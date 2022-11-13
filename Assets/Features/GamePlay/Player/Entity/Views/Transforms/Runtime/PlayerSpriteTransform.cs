using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Views.Transforms.Runtime
{
    public class PlayerSpriteTransform : TransformView, ISpriteTransform
    {
        [Inject]
        public void Construct(ILogger logger, IUpdater updater)
        {
            _updater = updater;
            
            CreateLogger(logger);
        }
        
        private IUpdater _updater;
        private Impact _last;

        public void Impact(Vector2 direction, float distance, float time)
        {
            if (_last is { IsEnded: false })
                return;
            
            _last = new Impact(_updater, this, direction, distance, time);
            
            _last.Start();
        }
    }
}