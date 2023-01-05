using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.PlayerCargos.UI;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.PlayerCargos.Bootstrap
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "PlayerCargo",
        menuName = GamePlayAssetsPaths.PlayerCargo + "Service")]
    public class PlayerCargoAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private UiConstraints _constraints;
        
        [SerializeField] [Indent] private PlayerCargo _prefab;
        [SerializeField] [Indent] private AssetReference _travelScene;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var cargo = Instantiate(_prefab);
            cargo.name = "PlayerCargo";

            var storage = cargo.GetComponent<PlayerCargoStorage>();

            builder.RegisterComponent(storage)
                .As<IPlayerCargoStorage>();
            
            builder.RegisterComponent(cargo)
                .As<IPlayerCargo>()
                .AsCallbackListener();

            serviceBinder.AddToModules(cargo);

            var uiSceneData = new TypedSceneLoadData<PlayerCargoUI>(_travelScene);
            var uiScene = await sceneLoader.Load(uiSceneData);
            var ui = uiScene.Searched;

            builder.RegisterComponent(ui)
                .WithParameter(_constraints)
                .AsCallbackListener();

            builder.Inject(ui.MoneyView);
        }
    }
}