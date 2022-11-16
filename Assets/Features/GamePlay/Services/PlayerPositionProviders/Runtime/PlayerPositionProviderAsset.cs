#region

using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;

#endregion

namespace GamePlay.Services.PlayerPositionProviders.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "PlayerPositionProvider",
        menuName = GamePlayAssetsPaths.PlayerPositionProvider + "Service")]
    public class PlayerPositionProviderAsset : LocalServiceAsset
    {
        [SerializeField] private PlayerPositionProvider _prefab;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var provider = Instantiate(_prefab);
            provider.name = "PlayerPositionProvider";

            serviceBinder.RegisterComponent(provider)
                .As<IPlayerTransformPresenter>()
                .As<IPlayerPositionProvider>();

            serviceBinder.AddToModules(provider);
        }
    }
}