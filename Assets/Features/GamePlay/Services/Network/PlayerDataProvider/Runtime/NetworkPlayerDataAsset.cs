#region

using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;

#endregion

namespace GamePlay.Services.Network.PlayerDataProvider.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "NetworkPlayerData",
        menuName = GamePlayAssetsPaths.NetworkPlayerData + "Service")]
    public class NetworkPlayerDataAsset : LocalServiceAsset
    {
        [SerializeField] private NetworkPlayerData _prefab;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var data = Instantiate(_prefab);
            data.name = "NetworkPlayerData";

            serviceBinder.RegisterComponent(data).AsImplementedInterfaces();

            serviceBinder.AddToModules(data);
        }
    }
}