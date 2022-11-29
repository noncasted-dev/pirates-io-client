using Common.ObjectsPools.Runtime;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Paths;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.VFX.Pool.Implementation.Dead
{
    [CreateAssetMenu(fileName = "FishVfx_PoolEntry", menuName = GamePlayAssetsPaths.VFX + "Fish")]
    public class FishVfxAsset : PoolEntryAsset
    {
        public override IObjectsPool Create(IObjectResolver resolver, Transform parent)
        {
            var instantiatorFactory = resolver.Resolve<IAssetInstantiatorFactory>();

            var factory = new FishVfxFactory(Reference, parent, instantiatorFactory);
            var provider = new ObjectProvider<FishVfx>(null, factory, StartupInstances, parent);

            return provider;
        }
    }
}