using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "TravelOverlay",
        menuName = GamePlayAssetsPaths.TravelOverlay + "Service")]
    public class TravelOverlayAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private UiConstraints _constraints;
        [SerializeField] [Indent] private AssetReference _uiScene;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var uiSceneData = new TypedSceneLoadData<TravelOverlay>(_uiScene);
            var uiScene = await sceneLoader.Load(uiSceneData);
            var overlay = uiScene.Searched;

            builder.RegisterComponent(overlay)
                .WithParameter(_constraints)
                .As<ITravelOverlay>()
                .AsSelf();

            var overlayBuilder = overlay.GetComponent<TravelOverlayBuilder>();
            callbacks.Listen(overlayBuilder);
        }
    }
}