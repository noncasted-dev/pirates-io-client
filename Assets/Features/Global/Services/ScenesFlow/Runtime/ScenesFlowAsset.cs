#region

using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.ScenesFlow.Logs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

#endregion

namespace Global.Services.ScenesFlow.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "ScenesFlow",
        menuName = GlobalAssetsPaths.ScenesFlow + "Service", order = 1)]
    public class ScenesFlowAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private ScenesFlowLogSettings _logSettings;
        [SerializeField] private ScenesLoader _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var loader = Instantiate(_prefab);
            loader.name = "ScenesFlow";

            var unloader = loader.GetComponent<ScenesUnloader>();

            builder.Register<ScenesFlowLogger>(Lifetime.Scoped).WithParameter("settings", _logSettings);
            builder.RegisterComponent(loader).AsImplementedInterfaces();
            builder.RegisterComponent(unloader).AsImplementedInterfaces();

            serviceBinder.AddToModules(loader);
            serviceBinder.ListenCallbacks(loader);
            serviceBinder.ListenCallbacks(unloader);
        }
    }
}