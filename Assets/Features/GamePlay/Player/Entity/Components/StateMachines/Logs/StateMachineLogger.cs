#region

using GamePlay.Player.Entity.States.Common;
using Global.Services.Loggers.Runtime;

#endregion

namespace GamePlay.Player.Entity.Components.StateMachines.Logs
{
    public class StateMachineLogger
    {
        public StateMachineLogger(ILogger logger, StateMachineLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly StateMachineLogSettings _settings;

        public void OnAvailabilityChecked(StateDefinition from, StateDefinition to, bool result)
        {
            if (_settings.IsAvailable(StateMachineLogType.AvailablityCheck) == false)
                return;

            _logger.Log($"Availability {from.name} -> {to.name} : {result}", _settings.LogParameters);
        }

        public void OnEnteredFirst(StateDefinition next)
        {
            if (_settings.IsAvailable(StateMachineLogType.Entered) == false)
                return;

            _logger.Log($"Entered {next.name}", _settings.LogParameters);
        }

        public void OnEnteredFrom(StateDefinition previous, StateDefinition next)
        {
            if (_settings.IsAvailable(StateMachineLogType.Entered) == false)
                return;

            _logger.Log($"Entered {next.name}, previous: {previous.name}", _settings.LogParameters);
        }

        public void OnExited(StateDefinition previous)
        {
            if (_settings.IsAvailable(StateMachineLogType.Exited) == false)
                return;

            _logger.Log($"Exited {previous.name}", _settings.LogParameters);
        }
    }
}