using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
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
            IdleDefinition definition,
            IdleLogger logger)
        {
            _stateMachine = stateMachine;
            _inertialMovement = inertialMovement;
            Definition = definition;
            _logger = logger;
        }

        private readonly IdleLogger _logger;

        private readonly IStateMachine _stateMachine;
        private readonly IInertialMovement _inertialMovement;

        public StateDefinition Definition { get; }

        public void Enter()
        {
            _stateMachine.Enter(this);
            _inertialMovement.Enable();

            _logger.OnEntered();
        }

        public void Break()
        {
            _inertialMovement.Disable();
            _logger.OnBroke();
        }
    }
}