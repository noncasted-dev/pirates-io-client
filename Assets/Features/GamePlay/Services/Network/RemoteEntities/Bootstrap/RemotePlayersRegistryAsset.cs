using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.Network.RemoteEntities.Storage;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Services.Network.RemoteEntities.Bootstrap
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "RemotePlayersStorage",
        menuName = GamePlayAssetsPaths.RemotePlayersStorage + "Service")]
    public class RemotePlayersRegistryAsset : LocalServiceAsset
    {
        [SerializeField] private RemotePlayersRegistry _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var storage = Instantiate(_prefab);
            storage.name = "RemotePlayersStorage";
            
            builder.RegisterComponent(storage)
                .As<IRemotePlayersRegistry>();

            serviceBinder.AddToModules(storage);
        }
    }
}