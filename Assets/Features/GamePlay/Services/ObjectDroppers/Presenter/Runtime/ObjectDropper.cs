#region

using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Items.Abstract;
using GamePlay.Services.ObjectDroppers.Implementation.Items.Runtime;
using GamePlay.Services.ObjectDroppers.Network.Runtime;
using GamePlay.Services.ObjectDroppers.Pool.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using Global.Services.ItemFactories.Runtime;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Services.ObjectDroppers.Presenter.Runtime
{
    [DisallowMultipleComponent]
    public class ObjectDropper :
        MonoBehaviour,
        IObjectDropper,
        ILocalSwitchListener,
        ILocalLoadListener
    {
        [Inject]
        private void Construct(
            INetworkObjectDropReceiver dropReceiver,
            INetworkObjectDropSender dropSender,
            IPlayerPositionProvider playerPositionProvider,
            IDropPoolProvider dropPoolProvider,
            IItemFactory itemFactory,
            ObjectDropperConfigAsset config)
        {
            _itemFactory = itemFactory;
            _config = config;
            _poolProvider = dropPoolProvider;
            _dropReceiver = dropReceiver;
            _dropSender = dropSender;
            _playerPositionProvider = playerPositionProvider;
        }

        private readonly DroppedObjectsStorage _storage = new();

        private IPlayerPositionProvider _playerPositionProvider;
        private INetworkObjectDropSender _dropSender;
        private INetworkObjectDropReceiver _dropReceiver;

        private IObjectProvider<IDroppedItem> _itemProvider;
        private IDropPoolProvider _poolProvider;
        private ObjectDropperConfigAsset _config;
        private IItemFactory _itemFactory;

        public void OnEnabled()
        {
            _dropReceiver.ItemDropped += OnNetworkItemDropReceived;
            _dropReceiver.ItemCollected += OnNetworkItemCollectReceived;
        }

        public void OnDisabled()
        {
            _dropReceiver.ItemDropped -= OnNetworkItemDropReceived;
            _dropReceiver.ItemCollected -= OnNetworkItemCollectReceived;
        }

        public void OnLoaded()
        {
            _itemProvider = _poolProvider.GetPool<IDroppedItem>(_config.DroppedItemPrefab);
        }

        public void DropFromPlayer(IItem item)
        {
            _dropSender.OnItemDropped(item, _playerPositionProvider.Position);
        }

        public void Drop(IItem item, Vector2 position)
        {
            _dropSender.OnItemDropped(item, position);
        }

        private void OnNetworkItemDropReceived(ItemDropEvent data)
        {
            var dropped = _itemProvider.Get(data.Position);
            var item = _itemFactory.Create(data.Type, data.Count);

            dropped.Construct(data.Id, OnLocalCollected, item);

            _storage.Add(data.Id, dropped);
        }

        private void OnNetworkItemCollectReceived(int id)
        {
            _storage.Remove(id);
        }

        private void OnLocalCollected(IDroppedItem item)
        {
            _dropSender.OnItemCollected(item.Id);
        }
    }
}