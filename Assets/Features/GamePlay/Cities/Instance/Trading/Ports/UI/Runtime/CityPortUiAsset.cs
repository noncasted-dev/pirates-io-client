using Common.EditableScriptableObjects.Attributes;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "CityPort",
        menuName = GamePlayAssetsPaths.CityPort + "Service")]
    public class CityPortUiAsset : LocalServiceAsset
    {
        [SerializeField] [EditableObject] private UiConstraints _constraints;
        [SerializeField] private AssetReference _uiScene;
        
        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var uiSceneData = new TypedSceneLoadData<CityPortUi>(_uiScene);
            var uiScene = await sceneLoader.Load(uiSceneData);

            serviceBinder.RegisterComponent(uiScene.Searched)
                .WithParameter(_constraints);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            resolver.Resolve<CityPortUi>();
        }
    }
}