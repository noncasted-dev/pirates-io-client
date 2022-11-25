using System;
using GamePlay.Items.Abstract;
using GamePlay.Services.Network.Common.EntityProvider.Runtime;
using GamePlay.Services.Network.PlayerDataProvider.Runtime;
using Ragon.Client;
using Ragon.Common;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.DroppedObjects.Network.Runtime
{
    public class ObjectDropNetworker :
        MonoBehaviour,
        INetworkObjectDropSender,
        INetworkObjectDropReceiver
    {
        [Inject]
        private void Construct(
            INetworkSessionEventSender sender,
            INetworkSessionEventListener listener,
            INetworkPlayerDataProvider playerDataProvider)
        {
            _playerDataProvider = playerDataProvider;
            _sender = sender;
            _listener = listener;

            _listener.AddListener<ItemDropEvent>(OnItemDropReceived);
            _listener.AddListener<ItemCollectedEvent>(OnItemCollectReceived);
        }

        private INetworkSessionEventListener _listener;
        private INetworkPlayerDataProvider _playerDataProvider;

        private INetworkSessionEventSender _sender;

        public event Action<ItemDropEvent> ItemDropped;
        public event Action<int> ItemCollected;

        public void OnItemDropped(ItemType type, int count, Vector2 origin, Vector2 target)
        {
            var data = new ItemDropEvent(
                type,
                origin,
                target,
                count,
                _playerDataProvider.GenerateUniqueId());

            _sender.ReplicateEvent(data, RagonTarget.All, RagonReplicationMode.LocalAndServer);
        }

        public void OnItemCollected(int id)
        {
            var data = new ItemCollectedEvent(id);

            _sender.ReplicateEvent(data, RagonTarget.All, RagonReplicationMode.Server);
        }

        private void OnItemDropReceived(RagonPlayer player, ItemDropEvent data)
        {
            Debug.Log($"Drop received: {data.Type}, {data.Count}, {data.Origin}, {data.Target}");

            ItemDropped?.Invoke(data);
        }

        private void OnItemCollectReceived(RagonPlayer player, ItemCollectedEvent data)
        {
            Debug.Log($"Drop collected: {data.Id}");
            
            ItemCollected?.Invoke(data.Id);
        }
    }
}