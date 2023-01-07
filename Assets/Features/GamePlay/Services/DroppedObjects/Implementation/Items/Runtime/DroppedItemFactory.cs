using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using Global.Services.AssetsFlow.Runtime.Abstract;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.DroppedObjects.Implementation.Items.Runtime
{
    public class DroppedItemFactory : IObjectFactory<DroppedItem>
    {
        public DroppedItemFactory(
            AssetReference reference,
            Transform parent,
            IAssetInstantiatorFactory instantiatorFactory,
            IUpdater updater)
        {
            _reference = reference;
            _parent = parent;
            _instantiatorFactory = instantiatorFactory;
            _updater = updater;
        }

        private readonly IAssetInstantiatorFactory _instantiatorFactory;
        private readonly Transform _parent;

        private readonly AssetReference _reference;
        private readonly IUpdater _updater;

        private IAssetInstantiator<DroppedItem> _instantiator;

        public async UniTask PreloadAsync()
        {
            _instantiator = _instantiatorFactory.Create<DroppedItem>(_reference);
            await _instantiator.PreloadAsync();
        }

        public void Unload()
        {
            _instantiator.Release();
        }

        public DroppedItem Create(Vector2 position, float angle = 0)
        {
            var item = _instantiator.Instantiate(position, angle, _parent);
            item.Construct(_updater);
            return item;
        }
    }
}