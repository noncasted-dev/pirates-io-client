using GamePlay.Services.Network.Common.Abstract;
using Local.Services.Abstract;
using UnityEngine;

namespace GamePlay.Services.Network.Bootstrap.Runtime
{
    public abstract class NetworkSessionServiceAsset : ScriptableObject
    {
        public abstract ISessionNetworkService Create(IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister);
    }
}