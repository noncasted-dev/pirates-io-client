using Cysharp.Threading.Tasks;
using GamePlay.Services.Network.Common.EntityProvider.Runtime;
using Local.Services.Abstract;
using Local.Services.Abstract.Callbacks;
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
            RagonNetwork.Room.SceneLoaded();
        }

        public void Bootstrap(IServiceBinder serviceBinder, ICallbacksRegister callbacksRegister)
        {
            serviceBinder.Register<NetworkEventsProvider>()
                .WithParameter<RagonBehaviour>(this)
                .As<INetworkSessionEventSender>()
                .As<INetworkSessionEventListener>();
        }
    }
}