using System;
using Common.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Components.ActionsStates.Runtime;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Player.Entity.Weapons.Handler.Runtime;
using GamePlay.Services.Projectiles.Selector.Runtime;
using UniRx;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack
{
    public class RangeAttack : IRangeAttack, IState
    {
        public RangeAttack(
            IStateMachine stateMachine,
            RangeAttackDefinition definition,
            IWeaponsHandler weapons,
            IInertialMovement inertialMovement,
            ISpriteRotation spriteRotation,
            IRangeAttackConfig config,
            IAimView aim,
            ISpriteTransform spriteTransform,
            IActionsStateProvider actionsStateProvider,
            IProjectileSelector selector,
            IShipResources resources,
            RangeAttackLogger logger)
        {
            _resources = resources;
            _stateMachine = stateMachine;
            Definition = definition;
            _weapons = weapons;
            _inertialMovement = inertialMovement;
            _spriteRotation = spriteRotation;
            _config = config;
            _aim = aim;
            _spriteTransform = spriteTransform;
            _actionsStateProvider = actionsStateProvider;
            _selector = selector;
            _delay = new AttackDelay(config);
            _logger = logger;
        }

        private readonly IActionsStateProvider _actionsStateProvider;
        private readonly IProjectileSelector _selector;

        private readonly IAimView _aim;
        private readonly IRangeAttackConfig _config;

        private readonly AttackDelay _delay;
        private readonly IInertialMovement _inertialMovement;
        private readonly RangeAttackLogger _logger;
        private readonly ISpriteRotation _spriteRotation;
        private readonly ISpriteTransform _spriteTransform;

        private readonly IStateMachine _stateMachine;
        private readonly IWeaponsHandler _weapons;
        
        private readonly IShipResources _resources;

        private bool _hasInput;
        private bool _isStarted;

        public bool IsAvailable => CheckAvailable();

        public void OnInput()
        {
            _hasInput = true;

            if (_actionsStateProvider.CanShoot == false)
                return;

            if (_isStarted == true)
                return;

            if (_stateMachine.IsAvailable(Definition) == false)
                return;

            if (_delay.IsAvailable() == false)
                return;

            if (_selector.CanShoot() == false)
                return;
            
            if (_resources.Cannons <= 0)
                return;

            Process().Forget();
        }

        public void OnInputCanceled()
        {
            _hasInput = false;
        }

        public void OnInputBroke()
        {
            if (_isStarted == false)
                return;

            _hasInput = false;
            _stateMachine.Exit();
        }

        public void Enter()
        {
            Process().Forget();
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
            _isStarted = false;

            _inertialMovement.Disable();
            _spriteRotation.Stop();
            _aim.OnBroke();

            _logger.OnBroke();
        }

        private async UniTaskVoid Process()
        {
            _stateMachine.Enter(this);

            _isStarted = true;
            _delay.OnAttack();
            _inertialMovement.Enable();
            _spriteRotation.Start();

            _logger.OnEntered();

            var aim = await _aim.AimAsync();

            if (_actionsStateProvider.CanShoot == false)
            {
                _hasInput = false;
                _stateMachine.Exit();
                return;
            }

            switch (aim.Type)
            {
                case AimResultType.Shot:
                    var impactParams = _config.CreateImpactParams();
                    var direction = -AngleUtils.ToDirection(aim.Angle);
                    _spriteTransform.Impact(direction, impactParams.Distance, impactParams.Time);

                    _weapons.Canon.Shoot(aim.Angle, aim.Spread);
                    MessageBroker.Default.Publish(new AimDelayEvent(_config.Delay));
                    break;
                case AimResultType.Broke:
                    _hasInput = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _stateMachine.Exit();
        }

        private bool CheckAvailable()
        {
            //_hasInput && _actionsStateProvider.CanShoot && _selector.CanShoot()

            if (_hasInput == false)
                return false;

            if (_actionsStateProvider.CanShoot == false)
                return false;

            if (_selector.CanShoot() == false)
                return false;

            if (_resources.Cannons <= 0)
                return false;

            return true;
        }
    }
}