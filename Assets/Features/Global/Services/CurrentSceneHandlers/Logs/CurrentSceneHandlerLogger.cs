using Global.Services.Loggers.Runtime;

namespace Global.Services.CurrentSceneHandlers.Logs
{
    public class CurrentSceneHandlerLogger
    {
        public CurrentSceneHandlerLogger(ILogger logger, CurrentSceneHandlerLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly CurrentSceneHandlerLogSettings _settings;

        public void OnLoaded(int scenesCount)
        {
            if (_settings.IsAvailable(CurrentSceneHandlerLogType.Load) == false)
                return;

            _logger.Log($"Loaded {scenesCount} scenes", _settings.LogParameters);
        }

        public void OnUnload(int scenesCount)
        {
            if (_settings.IsAvailable(CurrentSceneHandlerLogType.Unload) == false)
                return;

            _logger.Log($"Unloading {scenesCount} scenes", _settings.LogParameters);
        }

        public void OnNoCurrentSceneError()
        {
            if (_settings.IsAvailable(CurrentSceneHandlerLogType.NoCurrentSceneError) == false)
                return;

            _logger.Log("Unloading failed, no current scene assigned", _settings.LogParameters);
        }

        public void OnUnloadingFinalized()
        {
            if (_settings.IsAvailable(CurrentSceneHandlerLogType.FinalizeUnloading) == false)
                return;

            _logger.Log("Scenes unloading finalized", _settings.LogParameters);
        }
    }
}