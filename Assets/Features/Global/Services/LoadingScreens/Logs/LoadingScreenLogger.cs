using Global.Services.Loggers.Runtime;

namespace Global.Services.LoadingScreens.Logs
{
    public class LoadingScreenLogger
    {
        public LoadingScreenLogger(ILogger logger, LoadingScreenLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly LoadingScreenLogSettings _settings;

        public void OnShown()
        {
            if (_settings.IsAvailable(LoadingScreenLogType.Show) == false)
                return;

            _logger.Log("On shown", _settings.LogParameters);
        }

        public void OnHidden()
        {
            if (_settings.IsAvailable(LoadingScreenLogType.Hide) == false)
                return;

            _logger.Log("On hidden", _settings.LogParameters);
        }
    }
}