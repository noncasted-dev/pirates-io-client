using System;

namespace Features.GamePlay.Player.Entity.Network.Remote.View.State
{
    public interface IRemoteState
    {
        event Action Died;
    }
}