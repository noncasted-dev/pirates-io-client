using System.Threading;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.Respawns.Logs;

namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    public class Respawn : IState, IRespawn
    {
        public Respawn(
            IStateMachine stateMachine,
            RespawnLogger logger,
            StateDefinition definition)
        {
            _stateMachine = stateMachine;
            _logger = logger;
            Definition = definition;
        }

        private readonly RespawnLogger _logger;
        private readonly IStateMachine _stateMachine;

        private CancellationTokenSource _cancellation;

        public void Enter()
        {
            _stateMachine.Enter(this);

            _logger.OnEntered();

            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;

            _stateMachine.Exit();
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;

            _logger.OnBroke();
        }
    }
}