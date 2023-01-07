using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Factions.Selections.Loops.Runtime;
using GamePlay.Factions.Selections.UI.Runtime;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Factions.Selections.Bootstrap
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "FactionSelection",
        menuName = GamePlayAssetsPaths.FactionSelection + "Service")]
    public class FactionSelectionAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private UiConstraints _constraints;
        [SerializeField] [Indent] private FactionSelectionLoop _prefab;
        [SerializeField] [Indent] private AssetReference _uiScene;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var loop = Instantiate(_prefab);
            loop.name = "FactionSelection";

            builder.RegisterComponent(loop).As<IFactionSelectionLoop>();
            serviceBinder.AddToModules(loop);

            var uiSceneData = new TypedSceneLoadData<FactionSelectionUI>(_uiScene);
            var uiScene = await sceneLoader.Load(uiSceneData);

            builder.RegisterComponent(uiScene.Searched)
                .WithParameter(_constraints)
                .As<IFactionSelectionUI>();
        }
    }
}