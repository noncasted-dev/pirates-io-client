using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "TravelOverlay",
        menuName = GamePlayAssetsPaths.TravelOverlay + "Service")]
    public class TravelOverlayAsset : LocalServiceAsset
    {
        [SerializeField] private UiConstraints _constraints;
        [SerializeField] private AssetReference _uiScene;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var uiSceneData = new TypedSceneLoadData<TravelOverlay>(_uiScene);
            var uiScene = await sceneLoader.Load(uiSceneData);

            serviceBinder.RegisterComponent(uiScene.Searched)
                .WithParameter(_constraints)
                .As<ITravelOverlay>()
                .AsSelf();
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            var overlay = resolver.Resolve<TravelOverlay>();
            var builder = overlay.GetComponent<TravelOverlayBuilder>();
            
            builder.Register(resolver);
        }
    }
}