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

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "CityPort",
        menuName = GamePlayAssetsPaths.CityPort + "Service")]
    public class CityPortUiAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private UiConstraints _constraints;
        [SerializeField] [Indent] private AssetReference _uiScene;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var uiSceneData = new TypedSceneLoadData<CityPortUi>(_uiScene);
            var uiScene = await sceneLoader.Load(uiSceneData);

            var ui = uiScene.Searched;
            
            builder.RegisterComponent(ui)
                .WithParameter(_constraints);

            builder.RegisterComponent(ui.MoneyView)
                .AsCallbackListener();
            
            builder.RegisterComponent(ui.TradeHandler)
                .AsCallbackListener();
        }
    }
}