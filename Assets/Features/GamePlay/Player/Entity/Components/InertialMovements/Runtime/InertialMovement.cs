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
            InertialMovementConfigAsset config,
            InertialMovementLogger logger)
        {
            _rigidBody = rigidBody;
            _updater = updater;
            _config = config;
            _logger = logger;
        }
        
        private readonly IRigidBody _rigidBody;
        private readonly IUpdater _updater;
        private readonly InertialMovementConfigAsset _config;
        private readonly InertialMovementLogger _logger;

        private bool _isEnabled;

        private Vector2 _targetDirection;
        private Vector2 _startDirection;
        private Vector2 _currentDirection;

        private float _lerpTime;

        private float _speed;
        
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
            _speed = speed;
        }

        public void OnPreFixedUpdate(float delta)
        {
            _lerpTime += _speed * delta * _config.LerpSpeed;
            
            var progress = _config.Evaluate(_lerpTime, _startDirection, _targetDirection);
            _currentDirection = Vector2.Lerp(_startDirection, _targetDirection, progress);
            var speed = _speed * delta;
            
            _rigidBody.Move(_currentDirection, speed);
            
            _logger.OnMove(speed, _lerpTime, progress, _startDirection, _targetDirection, _currentDirection);
        }
    }
}