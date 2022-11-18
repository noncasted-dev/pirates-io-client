using Global.Services.InputViews.Constraints;
using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace Global.Services.InputViews.Logs
{
    public class InputViewLogger
    {
        public InputViewLogger(ILogger logger, InputViewLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly InputViewLogSettings _settings;

        public void OnMovementPressed(Vector2 value)
        {
            if (_settings.IsAvailable(InputViewLogType.MovementPressed) == false)
                return;

            _logger.Log($"Movement: {value} is pressed", _settings.LogParameters);
        }

        public void OnMovementCanceled()
        {
            if (_settings.IsAvailable(InputViewLogType.MovementCanceled) == false)
                return;

            _logger.Log("Movement: is canceled", _settings.LogParameters);
        }

        public void OnRangeAttackPerformed()
        {
            if (_settings.IsAvailable(InputViewLogType.RangeAttackPerformed) == false)
                return;

            _logger.Log("Range attack is pressed", _settings.LogParameters);
        }

        public void OnRangeAttackCanceled()
        {
            if (_settings.IsAvailable(InputViewLogType.RangeAttackCanceled) == false)
                return;

            _logger.Log("Range attack is canceled", _settings.LogParameters);
        }

        public void OnBeforeRebind()
        {
            if (_settings.IsAvailable(InputViewLogType.BeforeRebind) == false)
                return;

            _logger.Log("Before rebind", _settings.LogParameters);
        }

        public void OnAfterRebind()
        {
            if (_settings.IsAvailable(InputViewLogType.BeforeRebind) == false)
                return;

            _logger.Log("After rebind", _settings.LogParameters);
        }
        
        public void OnConstraintAdded(InputConstraints constraint, int count)
        {
            if (_settings.IsAvailable(InputViewLogType.ConstraintAdded) == false)
                return;

            _logger.Log($"Input constraint added: {constraint}, count: {count}", _settings.LogParameters);
        }
        
        public void OnConstraintReduced(InputConstraints constraint, int count)
        {
            if (_settings.IsAvailable(InputViewLogType.ConstraintReduced) == false)
                return;

            _logger.Log($"Input constraint reduced: {constraint}, count: {count}", _settings.LogParameters);
        }
        
        public void OnConstraintRemoved(InputConstraints constraint)
        {
            if (_settings.IsAvailable(InputViewLogType.ConstraintRemoved) == false)
                return;

            _logger.Log($"Input constraint removed: {constraint}", _settings.LogParameters);
        }
        
        public void OnConstraintBelowZeroException(InputConstraints constraint)
        {
            if (_settings.IsAvailable(InputViewLogType.ConstraintBelowZeroException) == false)
                return;

            _logger.Error($"Input constraint {constraint} cant be below zero", _settings.LogParameters);
        }
        
        public void OnInputCanceledWithConstraint(InputConstraints constraint)
        {
            if (_settings.IsAvailable(InputViewLogType.InputCanceledWithConstraint) == false)
                return;

            _logger.Log($"Input {constraint} canceled with constraint", _settings.LogParameters);
        }
    }
}