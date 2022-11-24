using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.PlayerCargos.UI.Travel;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace GamePlay.Services.PlayerCargos.Bootstrap
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "PlayerCargo",
        menuName = GamePlayAssetsPaths.PlayerCargo + "Service")]
    public class PlayerCargoAsset : LocalServiceAsset
    {
        [SerializeField] private PlayerCargo _prefab;
        [SerializeField] private AssetReference _travelScene;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var cargo = Instantiate(_prefab);
            cargo.name = "PlayerCargo";

            var storage = cargo.GetComponent<PlayerCargoStorage>();

            serviceBinder.RegisterComponent(storage)
                .As<IPlayerCargoStorage>();

            serviceBinder.AddToModules(cargo);
            callbacksRegister.ListenLoopCallbacks(cargo);

            var uiSceneData = new TypedSceneLoadData<PlayerTravelCargoUI>(_travelScene);
            var uiScene = await sceneLoader.Load(uiSceneData);
            var ui = uiScene.Searched;

            serviceBinder.RegisterComponent(ui);
            callbacksRegister.ListenLoopCallbacks(ui);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            resolver.Resolve<PlayerCargoStorage>();
            resolver.Resolve<PlayerTravelCargoUI>();
        }
    }
}