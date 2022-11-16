#region

using Global.Services.Loggers.Runtime;

#endregion

namespace Global.Services.GlobalCameras.Logs
{
    public class GlobalCameraLogger
    {
        public GlobalCameraLogger(ILogger logger, GlobalCameraLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly GlobalCameraLogSettings _settings;

        public void OnEnabled()
        {
            if (_settings.IsAvailable(GlobalCameraLogType.Enable) == false)
                return;

            _logger.Log("Enabled", _settings.LogParameters);
        }

        public void OnDisabled()
        {
            if (_settings.IsAvailable(GlobalCameraLogType.Disable) == false)
                return;

            _logger.Log("Disabled", _settings.LogParameters);
        }

        public void OnListenerEnabled()
        {
            if (_settings.IsAvailable(GlobalCameraLogType.EnableListener) == false)
                return;

            _logger.Log("Listener enabled", _settings.LogParameters);
        }

        public void OnListenerDisabled()
        {
            if (_settings.IsAvailable(GlobalCameraLogType.Disable) == false)
                return;

            _logger.Log("Listener disabled", _settings.LogParameters);
        }
    }
}