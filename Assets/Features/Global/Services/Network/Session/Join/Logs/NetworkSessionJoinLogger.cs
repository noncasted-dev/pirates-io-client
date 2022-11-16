#region

using Global.Services.Loggers.Runtime;

#endregion

namespace Global.Services.Network.Session.Join.Logs
{
    public class NetworkSessionJoinLogger
    {
        public NetworkSessionJoinLogger(ILogger logger, NetworkSessionJoinLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly NetworkSessionJoinLogSettings _settings;

        public void OnAttempted()
        {
            if (_settings.IsAvailable(NetworkSessionLogType.Attempt) == false)
                return;

            _logger.Log("Join attempt", _settings.LogParameters);
        }

        public void OnSuccess()
        {
            if (_settings.IsAvailable(NetworkSessionLogType.Success) == false)
                return;

            _logger.Log("Joined successfully", _settings.LogParameters);
        }

        public void OnFailed(string message)
        {
            if (_settings.IsAvailable(NetworkSessionLogType.Fail) == false)
                return;

            _logger.Log($"Join failed with message: {message}", _settings.LogParameters);
        }
    }
}