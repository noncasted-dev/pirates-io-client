using Cysharp.Threading.Tasks;
using Local.Services.Abstract;
using Local.Services.Abstract.Callbacks;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Services.Network.Bootstrap.Runtime
{
    [DisallowMultipleComponent]
    public class NetworkSessionBootstrapper : 
        MonoBehaviour,
        ILocalAsyncBootstrappedListener,
        INetworkSessionBootstrapper
    {
        public void Bootstrap(IServiceBinder serviceBinder, ICallbacksRegister callbacksRegister)
        {
            
        }
        
        public async UniTask OnBootstrappedAsync()
        {
            Debug.Log("On scene loaded");
            RagonNetwork.Room.SceneLoaded();
        }
    }
}