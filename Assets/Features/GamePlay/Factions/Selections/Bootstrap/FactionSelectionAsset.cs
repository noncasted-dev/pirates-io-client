using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Factions.Selections.Loops.Runtime;
using GamePlay.Factions.Selections.UI.Runtime;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Factions.Selections.Bootstrap
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "FactionSelection",
        menuName = GamePlayAssetsPaths.FactionSelection + "Service")]
    public class FactionSelectionAsset : LocalServiceAsset
    {
        [SerializeField] private FactionSelectionLoop _prefab;
        [SerializeField] private AssetReference _uiScene;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var loop = Instantiate(_prefab);
            loop.name = "FactionSelection";

            serviceBinder.RegisterComponent(loop).As<IFactionSelectionLoop>();
            serviceBinder.AddToModules(loop);

            var uiSceneData = new TypedSceneLoadData<FactionSelectionUI>(_uiScene);
            var uiScene = await sceneLoader.Load(uiSceneData);

            serviceBinder.RegisterComponent(uiScene.Searched)
                .As<IFactionSelectionUI>();
        }
    }
}