#region

using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.AssetsFlow.Logs;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

#endregion

namespace Global.Services.AssetsFlow.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "AssetsFlow",
        menuName = GlobalAssetsPaths.AssetsFlow + "Service", order = 1)]
    public class AssetsFlowAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private AssetsFlowLogSettings _logSettings;
        [SerializeField] private AssetLoader _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var loader = Instantiate(_prefab);
            loader.name = "AssetsFlow";

            var unloader = loader.GetComponent<AssetUnloader>();
            var storage = loader.GetComponent<AssetsReferencesStorage>();
            var instantiatorFabric = loader.GetComponent<AssetInstantiatorFactory>();

            builder.Register<AssetsFlowLogger>(Lifetime.Scoped).WithParameter("settings", _logSettings);

            builder.RegisterComponent(loader).AsImplementedInterfaces();
            builder.RegisterComponent(unloader).AsImplementedInterfaces();
            builder.RegisterComponent(storage).AsImplementedInterfaces();
            builder.RegisterComponent(instantiatorFabric).AsImplementedInterfaces();

            serviceBinder.AddToModules(loader);

            serviceBinder.ListenCallbacks(loader);
            serviceBinder.ListenCallbacks(unloader);
            serviceBinder.ListenCallbacks(storage);
            serviceBinder.ListenCallbacks(instantiatorFabric);
        }
    }
}