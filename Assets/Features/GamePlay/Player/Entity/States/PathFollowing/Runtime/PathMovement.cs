using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace Features.GamePlay.Player.Entity.States.PathFollowing.Runtime
{
    public class PathMovement : IPreFixedUpdatable
    {
        public PathMovement(
            IUpdater updater,
            IRigidBody rigidBody,
            ISpeedCalculator speedCalculator,
            Vector2[] path)
        {
            _updater = updater;
            _rigidBody = rigidBody;
            _speedCalculator = speedCalculator;
            _path = path;
        }

        private const float _reachedDistance = 0.1f;
        
        private readonly IUpdater _updater;
        private readonly IRigidBody _rigidBody;
        private readonly ISpeedCalculator _speedCalculator;
        private readonly Vector2[] _path;

        private int _currentTarget = 0;
        private bool _isUpdating = false;

        public void Start()
        {
            _isUpdating = true;
            _updater.Add(this);
        }

        public void Stop()
        {
            if (_isUpdating == false)
                return;
            
            _isUpdating = false;
            _updater.Remove(this);
        }

        public void OnPreFixedUpdate(float delta)
        {
            if (_currentTarget == _path.Length)
            {
                Stop();
                return;
            }

            var target = _path[_currentTarget];

            var direction = (target - _rigidBody.Position).normalized;
            var moveDistance = _speedCalculator.GetSpeed() * delta;
            
            _rigidBody.Move(direction, moveDistance);

            var distance = Vector2.Distance(_rigidBody.Position, target);

            if (distance <= _reachedDistance)
                _currentTarget++;
        }
    }
}