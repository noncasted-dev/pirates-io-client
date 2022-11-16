#region

using Global.Services.Loggers.Runtime;

#endregion

namespace GamePlay.Player.Entity.Views.Animators.Logs
{
    public class AnimatorLogger
    {
        public AnimatorLogger(ILogger logger, AnimatorLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly AnimatorLogSettings _settings;

        public void OnTrigger(string trigger)
        {
            if (_settings.IsAvailable(AnimatorLogType.Trigger) == false)
                return;

            _logger.Log($"Animator triggered: {trigger}", _settings.LogParameters);
        }

        public void OnFloat(string floatName, float value)
        {
            if (_settings.IsAvailable(AnimatorLogType.Float) == false)
                return;

            _logger.Log($"Float {floatName} set: {value}", _settings.LogParameters);
        }
    }
}