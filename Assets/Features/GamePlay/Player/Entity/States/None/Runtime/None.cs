#region

using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.None.Logs;
using GamePlay.Player.Entity.Views.Sprites.Runtime;

#endregion

namespace GamePlay.Player.Entity.States.None.Runtime
{
    public class None : INone, IState
    {
        public None(
            IStateMachine stateMachine,
            ISpriteSwitcher spriteSwitcher,
            NoneLogger logger,
            NoneDefinition definition)
        {
            _stateMachine = stateMachine;
            _spriteSwitcher = spriteSwitcher;
            _logger = logger;
            _definition = definition;
        }

        private readonly NoneDefinition _definition;
        private readonly NoneLogger _logger;
        private readonly ISpriteSwitcher _spriteSwitcher;

        private readonly IStateMachine _stateMachine;

        public void Enter()
        {
            _spriteSwitcher.Disable(true);

            _stateMachine.Enter(this);

            _logger.OnEntered();
        }

        public StateDefinition Definition => _definition;

        public void Break()
        {
            _spriteSwitcher.Enable(true);

            _logger.OnExited();
        }
    }
}