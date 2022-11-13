using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Services.Projectiles.Mover.Abstract;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.Projectiles.Implementation.Linear
{
    public class LinearProjectileFactory : IObjectFactory<LinearProjectile>
    {
        public LinearProjectileFactory(
            AssetReference reference,
            AssetReference waterSplash,
            IVfxPoolProvider vfx,
            Transform parent,
            IProjectilesMover mover,
            IAssetInstantiatorFactory instantiatorFactory)
        {
            _reference = reference;
            _waterSplash = waterSplash;
            _parent = parent;
            _mover = mover;
            _instantiatorFactory = instantiatorFactory;
            _vfx = vfx;
        }

        private readonly IAssetInstantiatorFactory _instantiatorFactory;
        private readonly IVfxPoolProvider _vfx;
        private readonly IProjectilesMover _mover;
        private readonly Transform _parent;

        private readonly AssetReference _reference;
        private readonly AssetReference _waterSplash;

        private IObjectProvider<AnimatedVfx> _waterSplashProvider;
        private IAssetInstantiator<LinearProjectile> _instantiator;

        public async UniTask PreloadAsync()
        {
            _waterSplashProvider = _vfx.GetPool<AnimatedVfx>(_waterSplash);
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
            projectile.Construct(_mover, _waterSplashProvider);

            return projectile;
        }
    }
}