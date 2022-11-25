using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Items.Abstract;
using GamePlay.Services.DroppedObjects.Implementation.Items.Runtime;
using GamePlay.Services.DroppedObjects.Network.Runtime;
using GamePlay.Services.DroppedObjects.Pool.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using Global.Services.ItemFactories.Runtime;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.DroppedObjects.Presenter.Runtime
{
    [DisallowMultipleComponent]
    public class DroppedObjectsPresenter :
        MonoBehaviour,
        IDroppedObjectsPresenter,
        ILocalSwitchListener,
        ILocalLoadListener
    {
        [Inject]
        private void Construct(
            INetworkObjectDropReceiver dropReceiver,
            INetworkObjectDropSender dropSender,
            IPlayerEntityProvider playerEntityProvider,
            IDropPoolProvider dropPoolProvider,
            IItemFactory itemFactory,
            ObjectDropperConfigAsset config)
        {
            _itemFactory = itemFactory;
            _config = config;
            _poolProvider = dropPoolProvider;
            _dropReceiver = dropReceiver;
            _dropSender = dropSender;
            _playerEntityProvider = playerEntityProvider;
        }

        private readonly DroppedObjectsStorage _storage = new();
        private ObjectDropperConfigAsset _config;
        private INetworkObjectDropReceiver _dropReceiver;
        private INetworkObjectDropSender _dropSender;
        private IItemFactory _itemFactory;

        private IObjectProvider<IDroppedItem> _itemProvider;

        private IPlayerEntityProvider _playerEntityProvider;
        private IDropPoolProvider _poolProvider;

        public void DropFromPlayer(ItemType type, int count)
        {
            var dropPosition = _playerEntityProvider.Position;
            dropPosition.y -= _config.DropFromPlayerYOffset;

            _dropSender.OnItemDropped(type, count, dropPosition);
        }

        public void Drop(ItemType type, int count, Vector2 position)
        {
            _dropSender.OnItemDropped(type, count, position);
        }

        public void OnLoaded()
        {
            _itemProvider = _poolProvider.GetPool<IDroppedItem>(_config.DroppedItemPrefab);
        }

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