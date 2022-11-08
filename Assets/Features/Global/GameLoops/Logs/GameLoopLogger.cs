using Global.Services.Loggers.Runtime;

namespace Global.GameLoops.Logs
{
    public class GameLoopLogger
    {
        public GameLoopLogger(ILogger logger, GameLoopLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly GameLoopLogSettings _settings;

        public void OnBegin()
        {
            if (_settings.IsAvailable(GameLoopLogType.Begin) == false)
                return;

            _logger.Log("Begin", _settings.LogParameters);
        }

        public void OnLoadLevel()
        {
            if (_settings.IsAvailable(GameLoopLogType.LoadLevel) == false)
                return;

            _logger.Log("Load level", _settings.LogParameters);
        }

        public void OnLoadMenu()
        {
            if (_settings.IsAvailable(GameLoopLogType.LoadMenu) == false)
                return;

            _logger.Log("Load menu", _settings.LogParameters);
        }
    }
}