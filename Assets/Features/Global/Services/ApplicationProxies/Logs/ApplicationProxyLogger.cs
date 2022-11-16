#region

using Global.Services.Loggers.Runtime;

#endregion

namespace Global.Services.ApplicationProxies.Logs
{
    public class ApplicationProxyLogger
    {
        public ApplicationProxyLogger(ILogger logger, ApplicationProxyLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly ApplicationProxyLogSettings _settings;

        public void OnQuit()
        {
            if (_settings.IsAvailable(ApplicationProxyLogType.Quit) == false)
                return;

            _logger.Log("Quit", _settings.LogParameters);
        }
    }
}