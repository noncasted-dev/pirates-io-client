using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.CurrentCameras.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.CurrentCameras.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "CurrentCamera",
        menuName = GlobalAssetsPaths.CurrentCamera + "Service", order = 1)]
    public class CurrentCameraAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private CurrentCameraLogSettings _logSettings;
        [SerializeField] private CurrentCamera _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var currentCamera = Instantiate(_prefab);
            currentCamera.name = "CurrentCamera";

            builder.Register<CurrentCameraLogger>(Lifetime.Singleton).WithParameter("settings", _logSettings);
            builder.RegisterComponent(currentCamera).AsImplementedInterfaces();

            serviceBinder.AddToModules(currentCamera);
            serviceBinder.ListenCallbacks(currentCamera);
        }
    }
}