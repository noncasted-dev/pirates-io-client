using System.Collections.Generic;
using GamePlay.Services.Network.RemoteEntities.Entity;

namespace GamePlay.Services.Network.RemoteEntities.Storage
{
    public interface IRemotePlayersRegistry
    {
        IEnumerable<IRemotePlayer> All { get; }
        bool Any { get; }

        void Add(IRemotePlayer player);
        void Remove(IRemotePlayer player);
        bool TryGet(string id, out IRemotePlayer player);
        IRemotePlayer[] Copy { get; }
    }
}