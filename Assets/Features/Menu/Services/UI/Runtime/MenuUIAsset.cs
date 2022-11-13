using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using Menu.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Menu.Services.UI.Runtime
{
    [CreateAssetMenu(fileName = MenuAssetsPaths.ServicePrefix + "UI", menuName = MenuAssetsPaths.UI)]
    public class MenuUIAsset : LocalServiceAsset
    {
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
                .As<IMenuUI>();

            serviceBinder.AddToModules(ui);
            callbacksRegister.ListenContainerCallbacks(ui);
        }
    }
}