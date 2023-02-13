using Features.GamePlay.Player.Entity.Network.Remote.View.Actions;
using Features.GamePlay.Player.Entity.Network.Remote.View.State;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Services.Network.RemoteEntities.Entity
{
    public class RemotePlayer : IRemotePlayer
    {
        public RemotePlayer(RagonEntity entity)
        {
            Entity = entity;
            _transform = entity.transform;
        }

        private readonly Transform _transform;

        public Vector2 Position => _transform.position;
        public RagonEntity Entity { get; }
        public IRemoteActions Actions { get; }
        public IRemoteState State { get; }
    }
}