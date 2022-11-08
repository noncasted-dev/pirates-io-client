using Global.Services.Loggers.Runtime;

namespace GamePlay.Services.LevelLoops.Logs
{
    public class LevelLoopLogger
    {
        public LevelLoopLogger(ILogger logger, LevelLoopLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly LevelLoopLogSettings _settings;

        public void OnLoaded()
        {
            if (_settings.IsAvailable(LevelLoopLogType.Loaded) == false)
                return;

            _logger.Log("Loaded", _settings.LogParameters);
        }

        public void OnPlayerSpawn()
        {
            if (_settings.IsAvailable(LevelLoopLogType.Loaded) == false)
                return;

            _logger.Log("Player spawned", _settings.LogParameters);
        }
    }
}