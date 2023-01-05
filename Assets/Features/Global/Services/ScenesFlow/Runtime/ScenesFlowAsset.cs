using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Global.Services.ScenesFlow.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.ScenesFlow.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "ScenesFlow",
        menuName = GlobalAssetsPaths.ScenesFlow + "Service")]
    public class ScenesFlowAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private ScenesFlowLogSettings _logSettings;
        [SerializeField] [Indent] private ScenesLoader _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var loader = Instantiate(_prefab);
            loader.name = "ScenesFlow";

            var unloader = loader.GetComponent<ScenesUnloader>();

            builder.Register<ScenesFlowLogger>()
                .WithParameter(_logSettings);

            builder.RegisterComponent(loader)
                .As<ISceneLoader>();

            builder.RegisterComponent(unloader)
                .As<ISceneUnloader>();

            serviceBinder.AddToModules(loader);
        }
    }
}