#region

using Global.Services.Loggers.Runtime;

#endregion

namespace GamePlay.Player.Entity.Components.Rotations.Logs
{
    public class RotationLogger
    {
        public RotationLogger(ILogger logger, RotationLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly RotationLogSettings _settings;

        public void OnRotationUsed(float angle)
        {
            if (_settings.IsAvailable(RotationLogType.Use) == false)
                return;

            _logger.Log($"Used: {angle}", _settings.LogParameters);
        }

        public void OnRotationSet(float angle)
        {
            if (_settings.IsAvailable(RotationLogType.Set) == false)
                return;

            _logger.Log($"Set: {angle}", _settings.LogParameters);
        }

        public void OnSpriteRotated(float angle)
        {
            if (_settings.IsAvailable(RotationLogType.SpriteRotate) == false)
                return;

            _logger.Log($"Sprite rotation: {angle}", _settings.LogParameters);
        }

        public void OnAnimatorRotated(float angle)
        {
            if (_settings.IsAvailable(RotationLogType.AnimatorRotate) == false)
                return;

            _logger.Log($"Animator rotation: {angle}", _settings.LogParameters);
        }
    }
}