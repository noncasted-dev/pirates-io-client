using Global.Services.Loggers.Runtime;

namespace Global.Services.Network.Connection.Logs
{
    public class NetworkConnectorLogger
    {
        public NetworkConnectorLogger(ILogger logger, NetworkConnectorLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly NetworkConnectorLogSettings _settings;

        public void OnAttempt(string ip, ushort port)
        {
            if (_settings.IsAvailable(NetworkConnectorLogType.Attempt) == false)
                return;

            _logger.Log($"Connection attempt: ip: {ip}, port: {port}", _settings.LogParameters);
        }

        public void OnSuccess()
        {
            if (_settings.IsAvailable(NetworkConnectorLogType.Success) == false)
                return;

            _logger.Log("Connected successfully", _settings.LogParameters);
        }

        public void OnFailed(string message)
        {
            if (_settings.IsAvailable(NetworkConnectorLogType.Fail) == false)
                return;

            _logger.Log($"Connected failed with message: {message}", _settings.LogParameters);
        }
    }
}