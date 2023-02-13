using Features.GamePlay.Player.Entity.Network.Remote.View.Actions;
using Features.GamePlay.Player.Entity.Network.Remote.View.State;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Services.Network.RemoteEntities.Entity
{
    public interface IRemotePlayer
    {
        Vector2 Position { get; }
        RagonEntity Entity { get; }
        
        IRemoteActions Actions { get; }
        IRemoteState State { get; }
    }
}