using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.AssetsFlow.Logs;
using Global.Services.AssetsFlow.Runtime.Abstract;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.AssetsFlow.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "AssetsFlow",
        menuName = GlobalAssetsPaths.AssetsFlow + "Service", order = 1)]
    public class AssetsFlowAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private AssetsFlowLogSettings _logSettings;
        [SerializeField] [Indent] private AssetLoader _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var loader = Instantiate(_prefab);
            loader.name = "AssetsFlow";

            var unloader = loader.GetComponent<AssetUnloader>();
            var storage = loader.GetComponent<AssetsReferencesStorage>();
            var instantiatorFabric = loader.GetComponent<AssetInstantiatorFactory>();

            builder.Register<AssetsFlowLogger>()
                .WithParameter(_logSettings);

            builder.RegisterComponent(loader)
                .As<IAssetLoader>()
                .AsCallbackListener();

            builder.RegisterComponent(unloader)
                .As<IAssetUnloader>()
                .AsCallbackListener();

            builder.RegisterComponent(storage)
                .As<IAssetsReferenceStorage>()
                .AsCallbackListener();

            builder.RegisterComponent(instantiatorFabric)
                .As<IAssetInstantiatorFactory>()
                .AsCallbackListener();

            serviceBinder.AddToModules(loader);
        }
    }
}