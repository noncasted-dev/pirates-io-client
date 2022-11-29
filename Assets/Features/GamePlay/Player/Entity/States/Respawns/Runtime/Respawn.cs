using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.Respawns.Logs;
using UniRx;

namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    public class Respawn : IState, IRespawn
    {
        public Respawn(
            IStateMachine stateMachine,
            IHealth health,
            RespawnLogger logger,
            RespawnConfigAsset config,
            RespawnDefinition definition)
        {
            _stateMachine = stateMachine;
            _health = health;
            _logger = logger;
            _config = config;
            Definition = definition;
        }

        private readonly RespawnConfigAsset _config;
        private readonly IHealth _health;

        private readonly RespawnLogger _logger;
        private readonly IStateMachine _stateMachine;

        public void Enter()
        {
            _stateMachine.Enter(this);

            _health.Respawn();

            _logger.OnEntered();
            _stateMachine.Exit();

            MessageBroker.Default.Publish(new PlayerRespawnedEvent(_health.Amount, _health.Max));
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
            _logger.OnBroke();
        }
    }
}