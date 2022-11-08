using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.Floating.Logs;
using GamePlay.Player.Entity.States.Idles.Runtime;
using GamePlay.Player.Entity.States.Runs.Runtime;

namespace GamePlay.Player.Entity.States.Floating.Runtime
{
    public class FloatingState : IFloatingState, ISwitchCallbacks
    {
        public FloatingState(
            IStateMachine stateMachine,
            IIdle idle,
            IRun run,
            FloatingStateLogger logger)
        {
            _stateMachine = stateMachine;

            _idle = idle;
            _run = run;

            _logger = logger;
        }

        private readonly IIdle _idle;

        private readonly FloatingStateLogger _logger;
        private readonly IRun _run;

        private readonly IStateMachine _stateMachine;

        public void Enter()
        {
            _logger.OnEntered();

            if (_run.HasInput == true)
            {
                _run.Enter();
                return;
            }

            _idle.Enter();
        }

        public void OnEnabled()
        {
            _stateMachine.Exited += Enter;
        }

        public void OnDisabled()
        {
            _stateMachine.Exited -= Enter;
        }
    }
}