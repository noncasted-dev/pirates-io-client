using Ragon.Client;
using Ragon.Common;

namespace GamePlay.Player.Entity.Network.Root.Runtime
{
    public interface IPlayerEventSender
    {
        public void ReplicateEvent<TEvent>(
            TEvent data,
            RagonTarget target = RagonTarget.All,
            RagonReplicationMode replicationMode = RagonReplicationMode.Server)
            where TEvent : IRagonEvent, new();
    }
}