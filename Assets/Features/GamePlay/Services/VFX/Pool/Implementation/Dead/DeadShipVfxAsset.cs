using Common.ObjectsPools.Runtime;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Paths;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.VFX.Pool.Implementation.Dead
{
    [CreateAssetMenu(fileName = "DeadShipVfx_PoolEntry", menuName = GamePlayAssetsPaths.VFX + "DeadShip")]
    public class DeadShipVfxAsset : PoolEntryAsset
    {
        public override IObjectsPool Create(IObjectResolver resolver, Transform parent)
        {
            var instantiatorFactory = resolver.Resolve<IAssetInstantiatorFactory>();

            var factory = new DeadShipVfxFactory(Reference, parent, instantiatorFactory);
            var provider = new ObjectProvider<DeadShipVfx>(null, factory, StartupInstances, parent);

            return provider;
        }
    }
}