using Common.ObjectsPools.Runtime;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Paths;
using GamePlay.Services.Projectiles.Mover;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.Projectiles.Implementation.Linear
{
    [CreateAssetMenu(fileName = "Projectile_Linear", menuName = GamePlayAssetsPaths.Projectiles + "Linear")]
    public class LinearProjectileAsset : PoolEntryAsset
    {
        public override string Name => "Arrow";

        public override IObjectsPool Create(IObjectResolver resolver, Transform parent)
        {
            var projectilesMover = resolver.Resolve<IProjectilesMover>();
            var instantiatorFactory = resolver.Resolve<IAssetInstantiatorFactory>();

            var factory = new LinearProjectileFactory(Reference, parent, projectilesMover, instantiatorFactory);
            var provider = new ObjectProvider<LinearProjectile>(null, factory, StartupInstances, parent);
            return provider;
        }
    }
}