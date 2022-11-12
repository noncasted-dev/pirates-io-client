using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Dash;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using GamePlay.Player.Entity.Weapons.Handler.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack
{
    public class RangeAttack : IRangeAttack, IState, ISwitchCallbacks
    {
        public RangeAttack(
            IStateMachine stateMachine,
            StateDefinition definition,
            IRangeAttackDash dash,
            IAnimatorView animatorView,
            IWeaponsHandler weapons,
            IRangeAttackRotator rotator,
            IInertialMovement inertialMovement,
            IRotation rotation,
            IRangeAttackConfig config,
            RangeAttackAnimatorCallbackBridge animatorCallback,
            AnimationTrigger animation,
            RangeAttackLogger logger)
        {
            _stateMachine = stateMachine;
            Definition = definition;
            _dash = dash;
            _animatorView = animatorView;
            _weapons = weapons;
            _rotator = rotator;
            _inertialMovement = inertialMovement;
            _rotation = rotation;
            _animatorCallback = animatorCallback;
            _delay = new AttackDelay(config);
            _animation = animation;
            _logger = logger;
        }

        private readonly AnimationTrigger _animation;
        private readonly RangeAttackAnimatorCallbackBridge _animatorCallback;
        private readonly IAnimatorView _animatorView;
        private readonly IRangeAttackDash _dash;
        private readonly AttackDelay _delay;
        private readonly RangeAttackLogger _logger;
        private readonly IRotation _rotation;
        private readonly IRangeAttackRotator _rotator;
        private readonly IInertialMovement _inertialMovement;

        private readonly IStateMachine _stateMachine;
        private readonly IWeaponsHandler _weapons;

        private CancellationTokenSource _cancellation;
        private Vector2 _direction;

        private bool _hasInput;

        private bool _isShot;
        private bool _isStarted;

        public bool HasInput => _hasInput;

        public void OnActionInput()
        {
            _hasInput = true;

            if (_isStarted == true)
                return;

            if (_stateMachine.IsAvailable(Definition) == false)
                return;

            if (_delay.IsAvailable() == false)
                return;

            Process().Forget();
        }

        public void OnAttackInputCanceled()
        {
            _hasInput = false;
        }

        public void Enter()
        {
            Process().Forget();
        }

        public void OnDirectionInput(Vector2 direction)
        {
            _direction = direction;
        }

        public void OnDirectionInputCanceled()
        {
            _direction = Vector2.zero;
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;
            _dash.Stop();
            _isStarted = false;

            _rotator.ToDefault();
            _inertialMovement.Disable();

            _logger.OnBroke();
        }

        public void OnEnabled()
        {
            _animatorCallback.ShootReady += OnShootReady;
        }

        public void OnDisabled()
        {
            _animatorCallback.ShootReady -= OnShootReady;
        }

        private async UniTaskVoid Process()
        {
            _stateMachine.Enter(this);

            _isStarted = true;
            _isShot = false;
            _delay.OnAttack();
            _inertialMovement.Enable();
            
            _logger.OnEntered();

            _rotator.Rotate(_direction);
            _dash.Start();

            _cancellation = new CancellationTokenSource();
            await _animatorView.PlayAsync(_animation, _animatorCallback, _cancellation.Token);

            _rotator.ToDefault();
            _stateMachine.Exit();
        }

        private void OnShootReady()
        {
            if (_isShot == true)
                return;

            _isShot = true;

            _logger.OnShootEvent();
            _weapons.Bow.Shoot(_rotation.Angle);
        }
    }
}