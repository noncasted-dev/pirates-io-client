using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace Global.Services.CameraUtilities.Logs
{
    public class CameraUtilsLogger
    {
        public CameraUtilsLogger(ILogger logger, CameraUtilsLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly CameraUtilsLogSettings _settings;

        public void OnScreenToWorld(Vector2 screen, Vector2 world)
        {
            if (_settings.IsAvailable(CameraUtilsLogType.ScreenToWorld) == false)
                return;

            _logger.Log($"Screen: {screen}, to world: {world}", _settings.LogParameters);
        }

        public void OnNoCameraError()
        {
            if (_settings.IsAvailable(CameraUtilsLogType.NoCameraError) == false)
                return;

            _logger.Error("No camera assigned", _settings.LogParameters);
        }
    }
}