using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.States.RangeAttacks.Logs
{
    public class RangeAttackLogger
    {
        public RangeAttackLogger(ILogger logger, RangeAttackLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly RangeAttackLogSettings _settings;

        public void OnAttackInput()
        {
            if (_settings.IsAvailable(RangeAttackLogType.InputAction) == false)
                return;

            _logger.Log("Attack input received", _settings.LogParameters);
        }

        public void OnAttackCanceled()
        {
            if (_settings.IsAvailable(RangeAttackLogType.ActionCanceled) == false)
                return;

            _logger.Log("Attack input canceled", _settings.LogParameters);
        }

        public void OnAttackBroke()
        {
            if (_settings.IsAvailable(RangeAttackLogType.ActionBroke) == false)
                return;

            _logger.Log("Attack input broke", _settings.LogParameters);
        }

        public void OnDirectionInput(Vector2 direction)
        {
            if (_settings.IsAvailable(RangeAttackLogType.InputDirection) == false)
                return;

            _logger.Log($"Direction input: {direction} received", _settings.LogParameters);
        }

        public void OnDirectionCanceled()
        {
            if (_settings.IsAvailable(RangeAttackLogType.DirectionCanceled) == false)
                return;

            _logger.Log("Direction input canceled", _settings.LogParameters);
        }

        public void OnEntered()
        {
            if (_settings.IsAvailable(RangeAttackLogType.Entered) == false)
                return;

            _logger.Log("On entered", _settings.LogParameters);
        }

        public void OnBroke()
        {
            if (_settings.IsAvailable(RangeAttackLogType.Broke) == false)
                return;

            _logger.Log("On broken", _settings.LogParameters);
        }

        public void OnDashStarted()
        {
            if (_settings.IsAvailable(RangeAttackLogType.DashStarted) == false)
                return;

            _logger.Log("Dash started", _settings.LogParameters);
        }

        public void OnDashBroke()
        {
            if (_settings.IsAvailable(RangeAttackLogType.DashBroke) == false)
                return;

            _logger.Log("Dash broke", _settings.LogParameters);
        }

        public void OnNoDashDirection()
        {
            if (_settings.IsAvailable(RangeAttackLogType.NoDashDirection) == false)
                return;

            _logger.Log("No dash direction", _settings.LogParameters);
        }

        public void OnDashProcessed(Vector2 direction, float distance, float time, float delta)
        {
            if (_settings.IsAvailable(RangeAttackLogType.DashProcessed) == false)
                return;

            _logger.Log($"Dash processed: direction: {direction}, distance: {distance}, time: {time}, delta: {delta}",
                _settings.LogParameters);
        }

        public void OnShootEvent()
        {
            if (_settings.IsAvailable(RangeAttackLogType.ShootEvent) == false)
                return;

            _logger.Log("Shoot event", _settings.LogParameters);
        }
    }
}