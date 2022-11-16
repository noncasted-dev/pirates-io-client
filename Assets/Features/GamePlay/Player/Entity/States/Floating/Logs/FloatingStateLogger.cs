#region

using Global.Services.Loggers.Runtime;

#endregion

namespace GamePlay.Player.Entity.States.Floating.Logs
{
    public class FloatingStateLogger
    {
        public FloatingStateLogger(ILogger logger, FloatingStateLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly FloatingStateLogSettings _settings;

        public void OnEntered()
        {
            if (_settings.IsAvailable(FloatingStateLogType.Entered) == false)
                return;

            _logger.Log("Entered: Floating", _settings.LogParameters);
        }
    }
}