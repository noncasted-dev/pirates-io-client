using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Menu.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Menu.Services.UI.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = MenuAssetsPaths.ServicePrefix + "UI", menuName = MenuAssetsPaths.UI)]
    public class MenuUIAsset : LocalServiceAsset
    {
        [SerializeField]  private UiConstraints _constraints;
        [SerializeField] private AssetReference _scene;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var request = new TypedSceneLoadData<MenuUI>(_scene);

            var result = await sceneLoader.Load(request);
            var ui = result.Searched;

            builder.RegisterComponent(ui)
                .WithParameter(_constraints)
                .As<IMenuUI>()
                .AsCallbackListener();

            serviceBinder.AddToModules(ui);
        }
    }
}