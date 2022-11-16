using System;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;

namespace GamePlay.Player.Entity.Components.StateMachines.Runtime
{
    public interface IStateMachine
    {
        event Action Exited;

        bool IsAvailable(StateDefinition definition);
        void Enter(IState state);
        void Exit();
    }
}