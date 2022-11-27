using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.ItemFactories.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "ItemFactory",
        menuName = GlobalAssetsPaths.ItemFactory + "Service")]
    public class ItemFactoryAsset : GlobalServiceAsset
    {
        [SerializeField]  private ItemFactoryConfigAsset _config;
        [SerializeField] private ItemFactory _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var factory = Instantiate(_prefab);
            factory.name = "ItemFactory";

            builder.RegisterComponent(factory)
                .WithParameter(_config)
                .As<IItemFactory>();

            serviceBinder.AddToModules(factory);
        }
    }
}