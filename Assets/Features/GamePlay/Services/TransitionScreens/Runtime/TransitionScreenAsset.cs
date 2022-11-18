using Common.EditableScriptableObjects.Attributes;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.TransitionScreens.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Local.Services.Abstract;
using UnityEngine;

namespace GamePlay.Services.TransitionScreens.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "TransitionScreen",
        menuName = GamePlayAssetsPaths.TransitionScreen + "Service")]
    public class TransitionScreenAsset : LocalServiceAsset
    {
        [SerializeField] [EditableObject] private TransitionScreenConfigAsset _config;
        [SerializeField] [EditableObject] private TransitionScreenLogSettings _logSettings;
        [SerializeField] [EditableObject] private UiConstraints _constraints;
        
        [SerializeField] private TransitionScreen _prefab;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var transitionScreen = Instantiate(_prefab);
            transitionScreen.name = "TransitionScreen";

            serviceBinder.Register<TransitionScreenLogger>()
                .WithParameter(_logSettings);
            
            serviceBinder.RegisterComponent(transitionScreen)
                .WithParameter(_config)
                .WithParameter(_constraints)
                .AsImplementedInterfaces();

            serviceBinder.AddToModules(transitionScreen);
        }
    }
}