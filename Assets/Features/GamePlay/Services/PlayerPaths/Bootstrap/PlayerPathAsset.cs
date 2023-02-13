using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.PlayerPaths.Builder.Runtime;
using GamePlay.Services.PlayerPaths.GameView.Runtime;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.PlayerPaths.Bootstrap
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "PlayerPath",
        menuName = GamePlayAssetsPaths.PlayerPath + "Service")]
    public class PlayerPathAsset : LocalServiceAsset
    {
        [SerializeField] private PlayerPathBuilder _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var pathBuilder = Instantiate(_prefab);
            pathBuilder.name = "PlayerPath";

            var gameView = pathBuilder.GetComponent<PlayerPathGameView>();

            builder.RegisterComponent(pathBuilder)
                .AsCallbackListener()
                .AsSelfResolvable();
            
            builder.RegisterComponent(gameView)
                .AsCallbackListener()
                .AsSelfResolvable();

            serviceBinder.AddToModules(pathBuilder);
        }
    }
}