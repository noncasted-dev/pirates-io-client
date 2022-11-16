#region

using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Updaters.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

#endregion

namespace Global.Services.Updaters.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "Updater",
        menuName = GlobalAssetsPaths.Updater + "Service", order = 1)]
    public class UpdaterAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private UpdaterLogSettings _logSettings;
        [SerializeField] private Updater _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var updater = Instantiate(_prefab);
            updater.name = "Updater";

            builder.Register<UpdaterLogger>(Lifetime.Scoped).WithParameter("settings", _logSettings);
            builder.RegisterComponent(updater).AsImplementedInterfaces();

            serviceBinder.AddToModules(updater);
            serviceBinder.ListenCallbacks(updater);
        }
    }
}