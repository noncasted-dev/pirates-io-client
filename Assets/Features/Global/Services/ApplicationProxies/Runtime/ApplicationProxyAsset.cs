#region

using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.ApplicationProxies.Logs;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

#endregion

namespace Global.Services.ApplicationProxies.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "ApplicationProxy",
        menuName = GlobalAssetsPaths.ApplicationProxy + "Service", order = 1)]
    public class ApplicationProxyAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private ApplicationProxyLogSettings _logSettings;
        [SerializeField] private ApplicationProxy _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var applicationProxy = Instantiate(_prefab);
            applicationProxy.name = "ApplicationProxy";

            builder.Register<ApplicationProxyLogger>(Lifetime.Scoped)
                .WithParameter("settings", _logSettings);

            builder.RegisterComponent(applicationProxy).AsImplementedInterfaces();

            serviceBinder.AddToModules(applicationProxy);
            serviceBinder.ListenCallbacks(applicationProxy);
        }
    }
}