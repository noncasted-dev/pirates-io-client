using Global.Services.Loggers.Runtime;

namespace GamePlay.Player.Entity.States.Idles.Logs
{
    public class IdleLogger
    {
        public IdleLogger(ILogger logger, IdleLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly IdleLogSettings _settings;

        public void OnEntered()
        {
            if (_settings.IsAvailable(IdleLogType.Entered) == false)
                return;

            _logger.Log("Entered: Idle", _settings.LogParameters);
        }

        public void OnBroke()
        {
            if (_settings.IsAvailable(IdleLogType.Broke) == false)
                return;

            _logger.Log("Broke: Idle", _settings.LogParameters);
        }
    }
}