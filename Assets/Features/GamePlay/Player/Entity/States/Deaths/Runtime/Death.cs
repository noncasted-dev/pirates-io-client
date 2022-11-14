﻿using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.Views.Sprites.Runtime;

namespace GamePlay.Player.Entity.States.Deaths.Runtime
{
    public class Death : IDeath, IState
    {
        public Death(
            IStateMachine stateMachine,
            DeathStateDefinition definition)
        {
            _stateMachine = stateMachine;
            Definition = definition;
        }
        
        private readonly IStateMachine _stateMachine;
        
        public StateDefinition Definition { get; }
        
        public void Enter()
        {
            _stateMachine.Enter(this);
        }
        
        public void Break()
        {
        }
    }
}