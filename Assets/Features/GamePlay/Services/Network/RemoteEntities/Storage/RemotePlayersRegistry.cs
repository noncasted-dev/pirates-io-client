using System.Collections.Generic;
using System.Collections.ObjectModel;
using GamePlay.Services.Network.RemoteEntities.Entity;
using UnityEngine;

namespace GamePlay.Services.Network.RemoteEntities.Storage
{   
    [DisallowMultipleComponent]
    public class RemotePlayersRegistry : MonoBehaviour, IRemotePlayersRegistry
    {
        private readonly List<IRemotePlayer> _list = new();
        private readonly Dictionary<string, IRemotePlayer> _dictionary;

        public IEnumerable<IRemotePlayer> All => new ReadOnlyCollection<IRemotePlayer>(_list);
        public bool Any => _list.Count != 0;
        public IRemotePlayer[] Copy => _list.ToArray();

        public void Add(IRemotePlayer player)
        {
            _list.Add(player);
            _dictionary.Add(player.Entity.Owner.Id, player);
        }

        public void Remove(IRemotePlayer player)
        {
            _list.Remove(player);
        }

        public bool TryGet(string id, out IRemotePlayer player)
        {
            return _dictionary.TryGetValue(id, out player);
        }
    }
}   