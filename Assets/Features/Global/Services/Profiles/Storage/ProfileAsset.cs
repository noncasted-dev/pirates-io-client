using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Global.Services.Profiles.Logs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Profiles.Storage
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "Profile",
        menuName = GlobalAssetsPaths.Profile + "Service")]
    public class ProfileAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private ProfileLogSettings _logSettings;
        [SerializeField] [Indent] private ProfileStorage _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var storage = Instantiate(_prefab);
            storage.name = "Profile";

            builder.Register<ProfileLogger>()
                .WithParameter(_logSettings);

            builder.RegisterComponent(storage)
                .As<IProfileStoragePresenter>()
                .As<IProfileStorageProvider>();

            serviceBinder.AddToModules(storage);
        }
    }
}