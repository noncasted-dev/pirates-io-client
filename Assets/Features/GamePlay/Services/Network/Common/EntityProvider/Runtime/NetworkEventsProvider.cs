#region

using System;
using Ragon.Client;
using Ragon.Common;

#endregion

namespace GamePlay.Services.Network.Common.EntityProvider.Runtime
{
    public class NetworkEventsProvider : INetworkSessionEventSender, INetworkSessionEventListener
    {
        public NetworkEventsProvider(RagonBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        private readonly RagonBehaviour _behaviour;

        public void AddListener<TEvent>(Action<RagonPlayer, TEvent> callback) where TEvent : IRagonEvent, new()
        {
            _behaviour.OnEvent(callback);
        }

        public void ReplicateEvent<TEvent>(TEvent data, RagonTarget target, RagonReplicationMode replicationMode)
            where TEvent : IRagonEvent, new()
        {
            _behaviour.ReplicateEvent(data, target, replicationMode);
        }
    }
}