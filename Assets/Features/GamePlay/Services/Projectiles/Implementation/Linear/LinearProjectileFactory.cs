using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Services.Projectiles.Mover;
using GamePlay.Services.Projectiles.Mover.Abstract;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.Projectiles.Implementation.Linear
{
    public class LinearProjectileFactory : IObjectFactory<LinearProjectile>
    {
        public LinearProjectileFactory(
            AssetReference reference,
            Transform parent,
            IProjectilesMover mover,
            IAssetInstantiatorFactory instantiatorFactory)
        {
            _reference = reference;
            _parent = parent;
            _mover = mover;
            _instantiatorFactory = instantiatorFactory;
        }

        private readonly IAssetInstantiatorFactory _instantiatorFactory;
        private readonly IProjectilesMover _mover;
        private readonly Transform _parent;

        private readonly AssetReference _reference;

        private IAssetInstantiator<LinearProjectile> _instantiator;

        public async UniTask PreloadAsync()
        {
            _instantiator = _instantiatorFactory.Create<LinearProjectile>(_reference);
            await _instantiator.PreloadAsync();
        }

        public void Unload()
        {
            _instantiator.Release();
        }

        public LinearProjectile Create(Vector2 position, float angle = 0)
        {
            var projectile = _instantiator.Instantiate(position, angle, _parent);
            projectile.Construct(_mover);

            return projectile;
        }
    }
}