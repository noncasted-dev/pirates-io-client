using System;
using Ragon.Client;

namespace GamePlay.Services.Network.Common.EntityProvider.Runtime
{
    public interface INetworkSessionEventListener
    {
        void AddListener<TEvent>(Action<RagonPlayer, TEvent> callback)
            where TEvent : IRagonEvent, new();
    }
}