using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.TransitionScreens.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.TransitionScreens.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "TransitionScreen",
        menuName = GamePlayAssetsPaths.TransitionScreen + "Service")]
    public class TransitionScreenAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private TransitionScreenConfigAsset _config;
        [SerializeField] [Indent] private TransitionScreenLogSettings _logSettings;
        [SerializeField] [Indent] private UiConstraints _constraints;

        [SerializeField] [Indent] private TransitionScreen _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var transitionScreen = Instantiate(_prefab);
            transitionScreen.name = "TransitionScreen";

            builder.Register<TransitionScreenLogger>()
                .WithParameter(_logSettings);

            builder.RegisterComponent(transitionScreen)
                .WithParameter(_config)
                .WithParameter(_constraints)
                .AsImplementedInterfaces();

            serviceBinder.AddToModules(transitionScreen);
        }
    }
}