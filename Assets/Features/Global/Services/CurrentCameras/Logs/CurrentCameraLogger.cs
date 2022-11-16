using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace Global.Services.CurrentCameras.Logs
{
    public class CurrentCameraLogger
    {
        public CurrentCameraLogger(ILogger logger, CurrentCameraLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly CurrentCameraLogSettings _settings;

        public void OnSetted(Camera camera)
        {
            if (_settings.IsAvailable(CurrentCameraLogType.Set) == false)
                return;

            _logger.Log($"Camera setted: {camera.name}", _settings.LogParameters);
        }

        public void OnUsed(Camera camera)
        {
            if (_settings.IsAvailable(CurrentCameraLogType.Use) == false)
                return;

            _logger.Log($"Camera used: {camera.name}", _settings.LogParameters);
        }
    }
}