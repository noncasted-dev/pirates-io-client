#region

using Common.ObjectsPools.Runtime;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Paths;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Services.DroppedObjects.Implementation.Items.Runtime
{
    [CreateAssetMenu(fileName = "DroppedItem_PoolEntry", menuName = GamePlayAssetsPaths.ObjectsDropper + "Item")]
    public class DroppedItemAsset : PoolEntryAsset
    {
        public override IObjectsPool Create(IObjectResolver resolver, Transform parent)
        {
            var instantiatorFactory = resolver.Resolve<IAssetInstantiatorFactory>();

            var factory = new DroppedItemFactory(Reference, parent, instantiatorFactory);
            var provider = new ObjectProvider<DroppedItem>(null, factory, StartupInstances, parent);

            return provider;
        }
    }
}