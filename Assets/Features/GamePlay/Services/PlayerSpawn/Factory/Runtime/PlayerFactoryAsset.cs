using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.PlayerSpawn.Factory.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "PlayerFactory",
        menuName = GamePlayAssetsPaths.PlayerFactory + "Service")]
    public class PlayerFactoryAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private PlayerFactoryLogSettings _logSettings;
        [SerializeField] [Indent] private PlayerFactoryConfigAsset _configAsset;
        [SerializeField] [Indent] private PlayerFactory _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var factory = Instantiate(_prefab);
            factory.name = "PlayerFactory";

            builder.Register<PlayerFactoryLogger>()
                .WithParameter(_logSettings);

            builder.RegisterComponent(factory)
                .WithParameter(_configAsset)
                .As<IPlayerFactory>()
                .AsCallbackListener();

            serviceBinder.AddToModules(factory);
        }
    }
}