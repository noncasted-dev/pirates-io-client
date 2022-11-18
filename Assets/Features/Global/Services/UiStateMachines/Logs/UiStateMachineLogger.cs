using Global.Services.Loggers.Runtime;

namespace Global.Services.UiStateMachines.Logs
{
    public class UiStateMachineLogger
    {
        public UiStateMachineLogger(ILogger logger, UiStateMachineLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly UiStateMachineLogSettings _settings;

        public void OnEnteredSingle(string name)
        {
            if (_settings.IsAvailable(UiStateMachineLogType.EnteredSingle) == false)
                return;

            _logger.Log($"Entered single: {name}", _settings.LogParameters);
        }
        
        public void OnEnteredStack(string head, string name)
        {
            if (_settings.IsAvailable(UiStateMachineLogType.EnteredStack) == false)
                return;

            _logger.Log($"Entered stack: from {head} to {name}", _settings.LogParameters);
        }
        
        public void OnExited(string name)
        {
            if (_settings.IsAvailable(UiStateMachineLogType.Exited) == false)
                return;

            _logger.Log($"Exited: {name}", _settings.LogParameters);
        }
        
        public void OnExitedStack(string name)
        {
            if (_settings.IsAvailable(UiStateMachineLogType.ExitedStack) == false)
                return;

            _logger.Log($"Exited stack: {name}", _settings.LogParameters);
        }
        
        public void OnRecovered(string name)
        {
            if (_settings.IsAvailable(UiStateMachineLogType.Recovered) == false)
                return;

            _logger.Log($"Recovered: {name}", _settings.LogParameters);
        }
        
        public void OnReturnedToPrevious(string from, string to)
        {
            if (_settings.IsAvailable(UiStateMachineLogType.ReturnedToPrevious) == false)
                return;

            _logger.Log($"Returned from {from} to {to}", _settings.LogParameters);
        }
    }
}