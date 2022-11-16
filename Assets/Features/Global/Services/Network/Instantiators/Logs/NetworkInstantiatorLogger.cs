#region

using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

#endregion

namespace Global.Services.Network.Instantiators.Logs
{
    public class NetworkInstantiatorLogger
    {
        public NetworkInstantiatorLogger(ILogger logger, NetworkInstantiatorLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly NetworkInstantiatorLogSettings _settings;

        public void OnRequested(Vector2 position, string prefabName)
        {
            if (_settings.IsAvailable(NetworkInstantiatorLogType.Request) == false)
                return;

            _logger.Log($"Instantiate requested: name: {prefabName}, position: {position}", _settings.LogParameters);
        }

        public void OnReturned(int id, string gameObjectName)
        {
            if (_settings.IsAvailable(NetworkInstantiatorLogType.Return) == false)
                return;

            _logger.Log($"Instantiated returned: name {gameObjectName}, id: {id}", _settings.LogParameters);
        }

        public void OnGetComponentException<T>(string gameObjectName)
        {
            if (_settings.IsAvailable(NetworkInstantiatorLogType.NoComponentException) == false)
                return;

            _logger.Log($"No component: {typeof(T)} found on {gameObjectName}", _settings.LogParameters);
        }
    }
}