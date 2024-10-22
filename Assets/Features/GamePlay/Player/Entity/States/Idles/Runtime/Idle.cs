﻿using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.Idles.Logs;

namespace GamePlay.Player.Entity.States.Idles.Runtime
{
    public class Idle : IState, IIdle
    {
        public Idle(
            IStateMachine stateMachine,
            IInertialMovement inertialMovement,
            ISpriteRotation spriteRotation,
            IdleDefinition definition,
            IdleLogger logger)
        {
            _stateMachine = stateMachine;
            _inertialMovement = inertialMovement;
            _spriteRotation = spriteRotation;
            Definition = definition;
            _logger = logger;
        }

        private readonly IInertialMovement _inertialMovement;

        private readonly IdleLogger _logger;
        private readonly ISpriteRotation _spriteRotation;

        private readonly IStateMachine _stateMachine;

        public void Enter()
        {
            _stateMachine.Enter(this);

            _inertialMovement.Enable();
            _spriteRotation.Start();

            _logger.OnEntered();
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
            _inertialMovement.Disable();
            _spriteRotation.Stop();

            _logger.OnBroke();
        }
    }
}