using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.ItemFactories.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "ItemFactory",
        menuName = GlobalAssetsPaths.ItemFactory + "Service")]
    public class ItemFactoryAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private ItemFactoryConfigAsset _config;
        [SerializeField] [Indent] private ItemFactory _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
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