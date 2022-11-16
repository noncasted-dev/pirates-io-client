#region

using Global.Common;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

#endregion

namespace Global.Services.PersistentInventories.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "PersistentInventory",
        menuName = GlobalAssetsPaths.PersistentInventory + "Service")]
    public class PersistentInventoryAsset : GlobalServiceAsset
    {
        [SerializeField] private PersistentInventory _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var PersistentInventory = Instantiate(_prefab);
            PersistentInventory.name = "PersistentInventory";

            builder.RegisterComponent(PersistentInventory).AsImplementedInterfaces();

            serviceBinder.AddToModules(PersistentInventory);
        }
    }
}