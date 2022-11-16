using Global.Services.Loggers.Runtime;

namespace GamePlay.Common.SceneObjects.Logs
{
    public class SceneObjectLogger
    {
        public SceneObjectLogger(ILogger logger, SceneObjectLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly SceneObjectLogSettings _settings;

        public void OnAwake(int objectsCount)
        {
            if (_settings.IsAvailable(SceneObjectLogType.Awake) == false)
                return;

            _logger.Log($"Awake called for {objectsCount} objects", _settings.LogParameters);
        }

        public void OnEnable(int objectsCount)
        {
            if (_settings.IsAvailable(SceneObjectLogType.Enable) == false)
                return;

            _logger.Log($"Enable called for {objectsCount} objects", _settings.LogParameters);
        }

        public void OnStart(int objectsCount)
        {
            if (_settings.IsAvailable(SceneObjectLogType.Start) == false)
                return;

            _logger.Log($"Start called for {objectsCount} objects", _settings.LogParameters);
        }

        public void OnDisable(int objectsCount)
        {
            if (_settings.IsAvailable(SceneObjectLogType.Disable) == false)
                return;

            _logger.Log($"Disable called for {objectsCount} objects", _settings.LogParameters);
        }

        public void OnDestroy(int objectsCount)
        {
            if (_settings.IsAvailable(SceneObjectLogType.Destroy) == false)
                return;

            _logger.Log($"Destroy called for {objectsCount} objects", _settings.LogParameters);
        }
    }
}