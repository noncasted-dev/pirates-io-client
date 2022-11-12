using Ragon.Client;
using Ragon.Common;

namespace GamePlay.Player.Entity.Network.Root.Runtime
{
    public interface INetworkEventSender
    {
        public void ReplicateEvent<TEvent>(
            TEvent evnt,
            RagonTarget target = RagonTarget.All,
            RagonReplicationMode replicationMode = RagonReplicationMode.Server)
            where TEvent : IRagonEvent, new();
    }
}