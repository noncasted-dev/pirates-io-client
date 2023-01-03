using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract.Callbacks;
using Cysharp.Threading.Tasks;
using GamePlay.Services.Network.Common.EntityProvider.Runtime;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Services.Network.Bootstrap.Runtime
{
    [DisallowMultipleComponent]
    public class NetworkSessionBootstrapper :
        RagonBehaviour,
        ILocalAsyncBootstrappedListener,
        INetworkSessionBootstrapper
    {
        public async UniTask OnBootstrappedAsync()
        {
            Debug.Log("[Network]:On scene loaded");
            RagonNetwork.Room.SceneLoaded();
        }

        public void Bootstrap(IDependencyRegister builder)
        {
            builder.Register<NetworkEventsProvider>()
                .WithParameter<RagonBehaviour>(this)
                .As<INetworkSessionEventSender>()
                .As<INetworkSessionEventListener>();
        }
    }
}