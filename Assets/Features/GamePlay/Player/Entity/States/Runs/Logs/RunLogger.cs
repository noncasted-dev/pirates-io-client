#region

using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

#endregion

namespace GamePlay.Player.Entity.States.Runs.Logs
{
    public class RunLogger
    {
        public RunLogger(ILogger logger, RunLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly RunLogSettings _settings;

        public void OnInput(Vector2 direction)
        {
            if (_settings.IsAvailable(RunLogType.Input) == false)
                return;

            _logger.Log($"On input: {direction}", _settings.LogParameters);
        }

        public void OnCanceled()
        {
            if (_settings.IsAvailable(RunLogType.Canceled) == false)
                return;

            _logger.Log("On canceled", _settings.LogParameters);
        }

        public void OnEnteredOnTrigger(Vector2 direction)
        {
            if (_settings.IsAvailable(RunLogType.EnteredOnTrigger) == false)
                return;

            _logger.Log($"Entered from trigger, direction: {direction}", _settings.LogParameters);
        }

        public void OnEnteredFromFloating(Vector2 direction)
        {
            if (_settings.IsAvailable(RunLogType.EnteredFromFloating) == false)
                return;

            _logger.Log($"Entered from floating, direction: {direction}", _settings.LogParameters);
        }

        public void OnEnterFromFloatingError()
        {
            if (_settings.IsAvailable(RunLogType.EnteredFromFloating) == false)
                return;

            _logger.Error("Direction is zero", _settings.LogParameters);
        }

        public void OnBroke()
        {
            if (_settings.IsAvailable(RunLogType.Broke) == false)
                return;

            _logger.Log("Broke", _settings.LogParameters);
        }
    }
}