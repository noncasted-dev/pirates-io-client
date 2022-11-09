using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.CameraUtilities.Logs;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.CameraUtilities.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "CameraUtils",
        menuName = GlobalAssetsPaths.CameraUtils + "Service")]
    public class CameraUtilsAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private CameraUtilsLogSettings _logSettings;
        [SerializeField] private CameraUtils _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var utils = Instantiate(_prefab);
            utils.name = "CameraUtils";

            builder.Register<CameraUtilsLogger>(Lifetime.Singleton).WithParameter("settings", _logSettings);
            builder.RegisterComponent(utils).AsImplementedInterfaces();

            serviceBinder.AddToModules(utils);
            serviceBinder.ListenCallbacks(utils);
        }
    }
}