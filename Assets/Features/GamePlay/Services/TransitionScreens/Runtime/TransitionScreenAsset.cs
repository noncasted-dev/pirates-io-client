#region

using Common.EditableScriptableObjects.Attributes;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.TransitionScreens.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;

#endregion

namespace GamePlay.Services.TransitionScreens.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "TransitionScreen",
        menuName = GamePlayAssetsPaths.TransitionScreen + "Service",
        order = 1)]
    public class TransitionScreenAsset : LocalServiceAsset
    {
        [SerializeField] private TransitionScreen _prefab;
        [SerializeField] [EditableObject] private TransitionScreenLogSettings _logSettings;
        [SerializeField] [EditableObject] private TransitionScreenConfigAsset _config;

        public override async UniTask Create(IServiceBinder serviceBinder, ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var transitionScreen = Instantiate(_prefab);
            transitionScreen.name = "TransitionScreen";

            serviceBinder.Register<TransitionScreenLogger>().WithParameter("settings", _logSettings);
            serviceBinder.RegisterComponent(transitionScreen)
                .WithParameter("config", _config)
                .AsImplementedInterfaces();

            serviceBinder.AddToModules(transitionScreen);
        }
    }
}