using Common.ObjectsPools.Runtime;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Paths;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.VFX.Pool.Implementation.Animated
{
    [CreateAssetMenu(fileName = "AnimatedVfx_PoolEntry", menuName = GamePlayAssetsPaths.VFX + "Animated")]
    public class AnimatedVfxAsset : PoolEntryAsset
    {
        public override IObjectsPool Create(IObjectResolver resolver, Transform parent)
        {
            var instantiatorFactory = resolver.Resolve<IAssetInstantiatorFactory>();

            var factory = new AnimatedVfxFactory(Reference, parent, instantiatorFactory);
            var provider = new ObjectProvider<AnimatedVfx>(null, factory, StartupInstances, parent);
            
            return provider;
        }
    }
}