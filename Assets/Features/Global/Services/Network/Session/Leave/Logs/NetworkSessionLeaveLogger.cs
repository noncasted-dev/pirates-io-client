using Global.Services.Loggers.Runtime;

namespace Global.Services.Network.Session.Leave.Logs
{
    public class NetworkSessionLeaveLogger
    {
        public NetworkSessionLeaveLogger(ILogger logger, NetworkSessionLeaveLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly NetworkSessionLeaveLogSettings _settings;

        public void OnAttempted()
        {
            if (_settings.IsAvailable(NetworkSessionLeaveLogType.Attempt) == false)
                return;

            _logger.Log($"Leave started", _settings.LogParameters);
        }
        
        public void OnSuccess()
        {
            if (_settings.IsAvailable(NetworkSessionLeaveLogType.Success) == false)
                return;

            _logger.Log($"Leave completed", _settings.LogParameters);
        }
        
        public void OnFailed(string message)
        {
            if (_settings.IsAvailable(NetworkSessionLeaveLogType.Fail) == false)
                return;

            _logger.Log($"Leave failed with message: {message}", _settings.LogParameters);
        }
    }
}