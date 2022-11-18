using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.ResourcesCleaners.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.ResourcesCleaners.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "ResourcesCleaner",
        menuName = GlobalAssetsPaths.ResourceCleaner + "Service", order = 1)]
    public class ResourcesCleanerAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private ResourcesCleanerLogSettings _logSettings;
        [SerializeField] private ResourcesCleaner _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var loadingScreen = Instantiate(_prefab);
            loadingScreen.name = "ResourcesCleaner";

            builder.RegisterComponent(loadingScreen).AsImplementedInterfaces();
            builder.Register<ResourcesCleanerLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            serviceBinder.AddToModules(loadingScreen);
            serviceBinder.ListenCallbacks(loadingScreen);
        }
    }
}