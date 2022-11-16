using GamePlay.Player.Entity.States.Common;

namespace GamePlay.Player.Entity.States.Abstract
{
    public interface IState
    {
        StateDefinition Definition { get; }

        void Break();
    }
}