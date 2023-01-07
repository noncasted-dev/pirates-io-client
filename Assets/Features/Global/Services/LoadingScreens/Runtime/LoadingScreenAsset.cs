using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Global.Services.LoadingScreens.Logs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Global.Services.LoadingScreens.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "LoadingScreen",
        menuName = GlobalAssetsPaths.LoadingScreen + "Service")]
    public class LoadingScreenAsset : GlobalServiceAsset
    {
        [SerializeField] [Indent] private LoadingScreenLogSettings _logSettings;
        [SerializeField] [Indent] private AssetReference _scene;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var result = await sceneLoader.Load(new InternalScene<LoadingScreen>(_scene));

            var loadingScreen = result.Searched;

            builder.Register<LoadingScreenLogger>()
                .WithParameter(_logSettings);

            builder.RegisterComponent(loadingScreen)
                .As<ILoadingScreen>();

            serviceBinder.AddToModules(loadingScreen);
        }
    }
}