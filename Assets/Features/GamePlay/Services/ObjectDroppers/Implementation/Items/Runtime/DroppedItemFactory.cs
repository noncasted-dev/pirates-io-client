#region

using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;

#endregion

namespace GamePlay.Services.ObjectDroppers.Implementation.Items.Runtime
{
    public class DroppedItemFactory : IObjectFactory<DroppedItem>
    {
        public DroppedItemFactory(
            AssetReference reference,
            Transform parent,
            IAssetInstantiatorFactory instantiatorFactory)
        {
            _reference = reference;
            _parent = parent;
            _instantiatorFactory = instantiatorFactory;
        }

        private readonly IAssetInstantiatorFactory _instantiatorFactory;
        private readonly Transform _parent;

        private readonly AssetReference _reference;

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

            return item;
        }
    }
}