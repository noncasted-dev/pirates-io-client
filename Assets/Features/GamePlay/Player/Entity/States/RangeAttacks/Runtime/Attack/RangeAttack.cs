using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.Weapons.Handler.Runtime;
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
            RangeAttackLogger logger)
        {
            _stateMachine = stateMachine;
            Definition = definition;
            _weapons = weapons;
            _inertialMovement = inertialMovement;
            _spriteRotation = spriteRotation;
            _aim = aim;
            _delay = new AttackDelay(config);
            _logger = logger;
        }

        private readonly AttackDelay _delay;
        private readonly IInertialMovement _inertialMovement;
        private readonly RangeAttackLogger _logger;
        private readonly ISpriteRotation _spriteRotation;
        private readonly IAimView _aim;

        private readonly IStateMachine _stateMachine;
        private readonly IWeaponsHandler _weapons;

        private bool _hasInput;

        private bool _isShot;
        private bool _isStarted;

        public bool HasInput => _hasInput;
        public StateDefinition Definition { get; }


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
            _isShot = false;
            _delay.OnAttack();
            _inertialMovement.Enable();
            _spriteRotation.Start();

            _logger.OnEntered();

            var aim = await _aim.AimAsync();

            if (aim.Type == AimResultType.Shot)
            {
                Debug.Log("Shot");
                _weapons.Canon.Shoot(aim.Angle, aim.Spread);
            }
            
            _stateMachine.Exit();
        }

        private void OnShootReady()
        {
            if (_isShot == true)
                return;

            _isShot = true;

            _logger.OnShootEvent();
            //
        }
    }
}