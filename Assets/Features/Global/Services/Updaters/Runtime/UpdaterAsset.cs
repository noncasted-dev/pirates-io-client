using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Global.Services.Updaters.Logs;
using Global.Services.Updaters.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Updaters.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "Updater",
        menuName = GlobalAssetsPaths.Updater + "Service")]
    public class UpdaterAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private UpdaterLogSettings _logSettings;
        [SerializeField] [Indent] private Updater _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var updater = Instantiate(_prefab);
            updater.name = "Updater";

            builder.Register<UpdaterLogger>()
                .WithParameter(_logSettings);

            builder.RegisterComponent(updater)
                .As<IUpdater>()
                .As<IUpdateSpeedModifier>()
                .As<IUpdateSpeedSetter>()
                .AsSelfResolvable()
                .AsCallbackListener();

            serviceBinder.AddToModules(updater);
        }
    }
}