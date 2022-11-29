using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

namespace GamePlay.Services.VFX.Pool.Implementation.Dead
{
    public class FishVfxFactory : IObjectFactory<FishVfx>
    {
        public FishVfxFactory(
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

        private IAssetInstantiator<FishVfx> _instantiator;

        public async UniTask PreloadAsync()
        {
            _instantiator = _instantiatorFactory.Create<FishVfx>(_reference);
            await _instantiator.PreloadAsync();
        }

        public void Unload()
        {
            _instantiator.Release();
        }

        public FishVfx Create(Vector2 position, float angle = 0)
        {
            var vfx = _instantiator.Instantiate(position, angle, _parent);
            
            Assert.IsNotNull(vfx);

            return vfx;
        }
    }
}