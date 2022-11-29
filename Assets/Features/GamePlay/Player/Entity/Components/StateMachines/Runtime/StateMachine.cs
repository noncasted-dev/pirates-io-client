using System;
using GamePlay.Player.Entity.Components.StateMachines.Logs;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;

namespace GamePlay.Player.Entity.Components.StateMachines.Runtime
{
    public class StateMachine : IStateMachine, ISwitchCallbacks
    {
        public StateMachine(StateMachineLogger logger)
        {
            _logger = logger;
        }

        private readonly StateMachineLogger _logger;

        private IState _current;

        public event Action Exited;

        public bool IsAvailable(StateDefinition definition)
        {
            if (_current == null)
                return true;

            var result = _current.Definition.IsTransitable(definition);

            _logger.OnAvailabilityChecked(_current.Definition, definition, result);

            return result;
        }

        public void Enter(IState state)
        {
            if (_current == null)
                _logger.OnEnteredFirst(state.Definition);
            else
                _logger.OnEnteredFrom(_current.Definition, state.Definition);

            _current?.Break();

            _current = state;
        }

        public void Exit()
        {
            _logger.OnExited(_current.Definition);
            Exited?.Invoke();
        }

        public void OnEnabled()
        {
        }

        public void OnDisabled()
        {
            _current?.Break();
        }
    }
}