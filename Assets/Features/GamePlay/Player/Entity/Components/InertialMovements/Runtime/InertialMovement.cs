using GamePlay.Player.Entity.Components.InertialMovements.Logs;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.InertialMovements.Runtime
{
    public class InertialMovement : IInertialMovement, IPreFixedUpdatable
    {
        public InertialMovement(
            IRigidBody rigidBody,
            IUpdater updater,
            ISpeedCalculator speedCalculator,
            InertialMovementConfigAsset config,
            InertialMovementLogger logger)
        {
            _rigidBody = rigidBody;
            _updater = updater;
            _speedCalculator = speedCalculator;
            _config = config;
            _logger = logger;

            _curve = new InertiaCurve(_config);
        }

        private readonly InertialMovementConfigAsset _config;
        private readonly InertiaCurve _curve;
        private readonly InertialMovementLogger _logger;

        private readonly IRigidBody _rigidBody;
        private readonly IUpdater _updater;
        private readonly ISpeedCalculator _speedCalculator;
        private Vector2 _currentDirection;

        private bool _isEnabled;

        private float _lerpTime;

        private Vector2 _startDirection;
        private Vector2 _targetDirection;

        public float XDirection => _currentDirection.x;

        public void Enable()
        {
            if (_isEnabled == true)
            {
                _logger.OnEnableTwiceError();
                return;
            }

            _logger.OnEnabled();

            _isEnabled = true;
            _updater.Add(this);
        }

        public void Disable()
        {
            if (_isEnabled == false)
            {
                _logger.OnDisableTwiceError();
                return;
            }

            _logger.OnDisabled();

            _isEnabled = false;
            _updater.Remove(this);
        }

        public void SetDirection(Vector2 direction)
        {
            _targetDirection = direction;
            _startDirection = _currentDirection;
            _lerpTime = 0f;

            _logger.OnDirectionSet(direction);
        }

        public void ResetDirection()
        {
            _targetDirection = Vector2.zero;
            _startDirection = _currentDirection;
            _lerpTime = 0f;

            _logger.OnDirectionReset();
        }

        public void SetSpeed(float speed)
        {
            _logger.OnSpeedSet(speed);
        }

        public void OnPreFixedUpdate(float delta)
        {
            return;
            var calculatedSpeed = _speedCalculator.GetSpeed();
            _lerpTime += calculatedSpeed * delta * _config.LerpSpeed;

            var progress = _curve.Evaluate(_lerpTime, _startDirection, _targetDirection);
            _currentDirection = Vector2.Lerp(_startDirection, _targetDirection, progress);
            var speed = calculatedSpeed * delta;

            _rigidBody.Move(_currentDirection, speed);

            _logger.OnMove(speed, _lerpTime, progress, _startDirection, _targetDirection, _currentDirection);
        }
    }
}