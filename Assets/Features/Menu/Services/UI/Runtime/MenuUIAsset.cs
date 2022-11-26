using Common.EditableScriptableObjects.Attributes;
using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Local.Services.Abstract;
using Menu.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Menu.Services.UI.Runtime
{
    [CreateAssetMenu(fileName = MenuAssetsPaths.ServicePrefix + "UI", menuName = MenuAssetsPaths.UI)]
    public class MenuUIAsset : LocalServiceAsset
    {
        [SerializeField]  private UiConstraints _constraints;
        [SerializeField] private AssetReference _scene;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var request = new TypedSceneLoadData<MenuUI>(_scene);

            var result = await sceneLoader.Load(request);
            var ui = result.Searched;

            serviceBinder.RegisterComponent(ui)
                .WithParameter(_constraints)
                .As<IMenuUI>();

            serviceBinder.AddToModules(ui);
            callbacksRegister.ListenContainerCallbacks(ui);
        }
    }
}