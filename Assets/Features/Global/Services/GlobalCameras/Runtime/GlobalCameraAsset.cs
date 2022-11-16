#region

using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.GlobalCameras.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

#endregion

namespace Global.Services.GlobalCameras.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "GlobalCamera",
        menuName = GlobalAssetsPaths.GlobalCamera + "Service", order = 1)]
    public class GlobalCameraAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private GlobalCameraLogSettings _logSettings;
        [SerializeField] private GlobalCamera _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var globalCamera = Instantiate(_prefab);
            globalCamera.name = "Camera_Global";
            globalCamera.gameObject.SetActive(false);

            builder.Register<GlobalCameraLogger>(Lifetime.Scoped)
                .WithParameter("settings", _logSettings);

            builder.RegisterComponent(globalCamera).AsImplementedInterfaces();

            serviceBinder.AddToModules(globalCamera);
            serviceBinder.ListenCallbacks(globalCamera);
        }
    }
}