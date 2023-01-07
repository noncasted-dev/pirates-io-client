using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.PlayerPositionProviders.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "PlayerPositionProvider",
        menuName = GamePlayAssetsPaths.PlayerPositionProvider + "Service")]
    public class PlayerPositionProviderAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private PlayerEntityProvider _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var provider = Instantiate(_prefab);
            provider.name = "PlayerPositionProvider";

            builder.RegisterComponent(provider)
                .As<IPlayerEntityPresenter>()
                .As<IPlayerEntityProvider>();

            serviceBinder.AddToModules(provider);
        }
    }
}