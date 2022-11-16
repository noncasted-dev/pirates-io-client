#region

using GamePlay.Player.Entity.States.Common;

#endregion

namespace GamePlay.Player.Entity.States.Abstract
{
    public interface IState
    {
        StateDefinition Definition { get; }

        void Break();
    }
}