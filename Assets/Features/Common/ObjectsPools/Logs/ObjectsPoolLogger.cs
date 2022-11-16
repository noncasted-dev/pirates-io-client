using Global.Services.Loggers.Runtime;

namespace Common.ObjectsPools.Logs
{
    public class ObjectsPoolLogger
    {
        public ObjectsPoolLogger(ILogger logger, ObjectsPoolLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly ObjectsPoolLogSettings _settings;

        public void OnPoolCreated(string objectName)
        {
            if (_settings.IsAvailable(ObjectsPoolLogType.PoolCreated) == false)
                return;

            _logger.Log($"Pool for: {objectName} created", _settings.LogParameters);
        }

        public void OnObjectCreated(string objectName)
        {
            if (_settings.IsAvailable(ObjectsPoolLogType.ObjectCreated) == false)
                return;

            _logger.Log($"Object: {objectName} created", _settings.LogParameters);
        }

        public void OnInactiveObjectCreated(string objectName)
        {
            if (_settings.IsAvailable(ObjectsPoolLogType.InactiveObjectTaken) == false)
                return;

            _logger.Log($"Inactive object: {objectName} taken", _settings.LogParameters);
        }

        public void OnActiveObjectReturned(string objectName)
        {
            if (_settings.IsAvailable(ObjectsPoolLogType.ActiveObjectReturned) == false)
                return;

            _logger.Log($"Active object: {objectName} returned", _settings.LogParameters);
        }
    }
}