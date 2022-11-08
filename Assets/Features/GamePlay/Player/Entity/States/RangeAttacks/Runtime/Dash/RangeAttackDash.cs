using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Dash
{
    public class RangeAttackDash : IRangeAttackDash, IFixedUpdatable
    {
        public RangeAttackDash(
            IUpdater updater,
            IRangeAttackConfig config,
            IRigidBody rigidBody,
            IDashDirection direction,
            RangeAttackLogger logger)
        {
            _updater = updater;
            _config = config;
            _rigidBody = rigidBody;
            _direction = direction;
            _logger = logger;
        }

        private const float _directionSwitchTime = 0.1f;
        private readonly IRangeAttackConfig _config;
        private readonly IDashDirection _direction;
        private readonly RangeAttackLogger _logger;
        private readonly IRigidBody _rigidBody;

        private readonly IUpdater _updater;
        private float _directionSwitchTimer;

        private bool _isStarted;

        private DashParams _params;
        private float _previousEvaluate;

        private Vector2 _start;
        private float _time;

        public void OnFixedUpdate(float delta)
        {
            _direction.Evaluate(delta);

            var evaluate = _params.Evaluate(_time);
            var speed = _params.Distance * (evaluate - _previousEvaluate);

            _previousEvaluate = evaluate;
            _rigidBody.Move(_direction.Current, speed);

            var distance = Vector2.Distance(_start, _rigidBody.Position);

            _logger.OnDashProcessed(_direction.Current, distance, _time, speed);

            _time += delta;
        }

        public void Start()
        {
            if (_direction.Current.Equals(Vector2.zero) == true)
            {
                _logger.OnNoDashDirection();
                return;
            }

            if (_isStarted == true)
                return;

            _isStarted = true;
            _params = _config.CreateDashParams();

            _time = 0f;
            _previousEvaluate = 0f;

            _direction.OnStarted();
            _start = _rigidBody.Position;
            _logger.OnDashStarted();
            _updater.Add(this);
        }

        public void Stop()
        {
            if (_isStarted == false)
                return;

            _isStarted = false;

            _updater.Remove(this);
            _direction.OnStopped();

            _logger.OnDashBroke();
        }
    }
}