using Global.Services.Loggers.Runtime;

namespace Global.Services.ResourcesCleaners.Logs
{
    public class ResourcesCleanerLogger
    {
        public ResourcesCleanerLogger(ILogger logger, ResourcesCleanerLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private const string _header = "ScenesFlow";

        private readonly ILogger _logger;
        private readonly ResourcesCleanerLogSettings _settings;

        public void OnCleaned()
        {
            if (_settings.IsAvailable(ResourcesCleanerLogType.Cleaned) == false)
                return;

            _logger.Log("Resources are cleaned", _settings.LogParameters);
        }
    }
}