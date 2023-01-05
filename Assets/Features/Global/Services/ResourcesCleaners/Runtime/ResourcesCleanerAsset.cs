using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Global.Services.ResourcesCleaners.Logs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.ResourcesCleaners.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "ResourcesCleaner",
        menuName = GlobalAssetsPaths.ResourceCleaner + "Service")]
    public class ResourcesCleanerAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private ResourcesCleanerLogSettings _logSettings;
        [SerializeField] [Indent] private ResourcesCleaner _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var resourcesCleaner = Instantiate(_prefab);
            resourcesCleaner.name = "ResourcesCleaner";

            builder.Register<ResourcesCleanerLogger>().WithParameter(_logSettings);

            builder.RegisterComponent(resourcesCleaner)
                .As<IResourcesCleaner>();

            serviceBinder.AddToModules(resourcesCleaner);
        }
    }
}