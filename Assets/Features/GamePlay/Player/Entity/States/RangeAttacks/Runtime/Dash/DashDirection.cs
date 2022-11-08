using Common.Structs;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Dash
{
    public class DashDirection : IDashDirection
    {
        private const float _directionSwitchTime = 0.1f;

        private Vector2 _currentDirection;
        private float _directionSwitchTimer;

        private bool _isDirectionSwitched;
        private bool _isStarted;
        private Vector2 _previousDirection;
        private Vector2 _targetDirection;

        public Vector2 Current => _currentDirection;

        public void OnDirectionInput(Vector2 direction)
        {
            _targetDirection = direction;

            if (_isStarted == false || _currentDirection == Vector2.zero)
                _currentDirection = direction;

            if (_isStarted == false)
                return;

            if (_currentDirection.IsAlong(direction) == false)
                return;

            _previousDirection = _currentDirection;
            _targetDirection = direction;
            _isDirectionSwitched = true;
            _directionSwitchTimer = 0f;
        }

        public void OnDirectionInputCanceled()
        {
            _currentDirection = Vector2.zero;
            _targetDirection = Vector2.zero;

            _isDirectionSwitched = false;
        }

        public void OnStarted()
        {
            _isStarted = true;
            _currentDirection = _targetDirection;
            _directionSwitchTimer = 0f;
            _isDirectionSwitched = false;
            _previousDirection = Vector2.zero;
        }

        public void OnStopped()
        {
            _isStarted = false;
        }

        public void Evaluate(float delta)
        {
            if (_isDirectionSwitched == false)
                return;

            if (_isStarted == false)
                return;

            _directionSwitchTimer += delta;
            var progress = _directionSwitchTimer / _directionSwitchTime;
            _currentDirection = Vector2.Lerp(_previousDirection, _targetDirection, progress);

            if (progress >= 1f)
                _isDirectionSwitched = false;
        }
    }
}