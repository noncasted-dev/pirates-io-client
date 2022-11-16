#region

using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

#endregion

namespace GamePlay.Player.Entity.Components.InertialMovements.Logs
{
    public class InertialMovementLogger
    {
        public InertialMovementLogger(ILogger logger, InertialMovementLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly InertialMovementLogSettings _settings;

        public void OnDirectionSet(Vector2 direction)
        {
            if (_settings.IsAvailable(InertialMovementType.DirectionSet) == false)
                return;

            _logger.Log($"Direction assigned: {direction}", _settings.LogParameters);
        }

        public void OnDirectionReset()
        {
            if (_settings.IsAvailable(InertialMovementType.DirectionReset) == false)
                return;

            _logger.Log("Direction reset", _settings.LogParameters);
        }

        public void OnSpeedSet(float speed)
        {
            if (_settings.IsAvailable(InertialMovementType.SpeedSet) == false)
                return;

            _logger.Log($"Speed assigned: {speed}", _settings.LogParameters);
        }

        public void OnMove(
            float speed,
            float lerp,
            float progress,
            Vector2 startDirection,
            Vector2 targetDirection,
            Vector2 resultDirection)
        {
            if (_settings.IsAvailable(InertialMovementType.Move) == false)
                return;

            _logger.Log($"Moved: speed: {speed}" +
                        $" Lerp: {lerp}" +
                        $" Progress: {progress}" +
                        $" Start: {startDirection}" +
                        $" Target: {targetDirection}" +
                        $" Result: {resultDirection}", _settings.LogParameters);
        }

        public void OnEnabled()
        {
            if (_settings.IsAvailable(InertialMovementType.Enable) == false)
                return;

            _logger.Log("Enabled", _settings.LogParameters);
        }

        public void OnDisabled()
        {
            if (_settings.IsAvailable(InertialMovementType.Disable) == false)
                return;

            _logger.Log("Disabled", _settings.LogParameters);
        }

        public void OnEnableTwiceError()
        {
            if (_settings.IsAvailable(InertialMovementType.EnableTwiceError) == false)
                return;

            _logger.Error("Trying to enable twice", _settings.LogParameters);
        }

        public void OnDisableTwiceError()
        {
            if (_settings.IsAvailable(InertialMovementType.DisableTwiceError) == false)
                return;

            _logger.Error("Trying to disable twice", _settings.LogParameters);
        }
    }
}