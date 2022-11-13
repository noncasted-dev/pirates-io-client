using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Profiles.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.Profiles.Storage
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "Profile",
        menuName = GlobalAssetsPaths.Profile + "Service")]
    public class ProfileAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private ProfileLogSettings _logSettings;
        [SerializeField] private ProfileStorage _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var storage = Instantiate(_prefab);
            storage.name = "Profile";

            builder.Register<ProfileLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            builder.RegisterComponent(storage)
                .As<IProfileStoragePresenter>()
                .As<IProfileStorageProvider>();

            serviceBinder.AddToModules(storage);
        }
    }
}