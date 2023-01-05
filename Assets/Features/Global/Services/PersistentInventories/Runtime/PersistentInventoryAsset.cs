using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.PersistentInventories.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "PersistentInventory",
        menuName = GlobalAssetsPaths.PersistentInventory + "Service")]
    public class PersistentInventoryAsset : GlobalServiceAsset
    {
        [SerializeField] private PersistentInventory _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var PersistentInventory = Instantiate(_prefab);
            PersistentInventory.name = "PersistentInventory";

            builder.RegisterComponent(PersistentInventory).AsImplementedInterfaces();

            serviceBinder.AddToModules(PersistentInventory);
        }
    }
}