using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.LoadingScreens.Logs;
using Global.Services.UiStateMachines.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.LoadingScreens.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "LoadingScreen",
        menuName = GlobalAssetsPaths.LoadingScreen + "Service", order = 1)]
    public class LoadingScreenAsset : GlobalServiceAsset
    {
        [SerializeField] [EditableObject] private LoadingScreenLogSettings _logSettings;
        [SerializeField] [EditableObject] private UiConstraints _constraints; 
        
        [SerializeField] private LoadingScreen _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var loadingScreen = Instantiate(_prefab);
            loadingScreen.name = "LoadingScreen";
            loadingScreen.gameObject.SetActive(false);

            builder.Register<LoadingScreenLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            builder.RegisterComponent(loadingScreen)
                .WithParameter(_constraints)
                .AsImplementedInterfaces();

            serviceBinder.AddToModules(loadingScreen);
            serviceBinder.ListenCallbacks(loadingScreen);
        }
    }
}