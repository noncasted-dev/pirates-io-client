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
            ISpriteRotation spriteRotation,
            StateDefinition definition,
            IdleLogger logger)
        {
            _stateMachine = stateMachine;
            Definition = definition;
            _logger = logger;
        }

        private readonly IdleLogger _logger;

        private readonly IStateMachine _stateMachine;
        
        public StateDefinition Definition { get; }

        public void Enter()
        {
            _stateMachine.Enter(this);

            _logger.OnEntered();
        }

        public void Break()
        {
            _logger.OnBroke();
        }
    }
}