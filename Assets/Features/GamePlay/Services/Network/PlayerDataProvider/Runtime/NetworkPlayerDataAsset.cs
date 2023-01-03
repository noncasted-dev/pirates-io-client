using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.Network.PlayerDataProvider.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "NetworkPlayerData",
        menuName = GamePlayAssetsPaths.NetworkPlayerData + "Service")]
    public class NetworkPlayerDataAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private NetworkPlayerData _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var data = Instantiate(_prefab);
            data.name = "NetworkPlayerData";

            builder
                .RegisterComponent(data)
                .AsImplementedInterfaces();

            serviceBinder.AddToModules(data);
        }
    }
}