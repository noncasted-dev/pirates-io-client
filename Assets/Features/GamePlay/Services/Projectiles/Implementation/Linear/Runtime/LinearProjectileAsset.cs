using Common.ObjectsPools.Runtime;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Paths;
using GamePlay.Services.Projectiles.Mover.Abstract;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace GamePlay.Services.Projectiles.Implementation.Linear.Runtime
{
    [CreateAssetMenu(fileName = "Projectile_Linear", menuName = GamePlayAssetsPaths.Projectiles + "Linear")]
    public class LinearProjectileAsset : PoolEntryAsset
    {
        [SerializeField] private AssetReference _waterSplash;

        public override IObjectsPool Create(IObjectResolver resolver, Transform parent)
        {
            var projectilesMover = resolver.Resolve<IProjectilesMover>();
            var instantiatorFactory = resolver.Resolve<IAssetInstantiatorFactory>();
            var vfxProvider = resolver.Resolve<IVfxPoolProvider>();

            var factory = new LinearProjectileFactory(
                Reference,
                _waterSplash,
                vfxProvider,
                parent, projectilesMover, instantiatorFactory);
            var provider = new ObjectProvider<LinearProjectile>(null, factory, StartupInstances, parent);
            return provider;
        }
    }
}