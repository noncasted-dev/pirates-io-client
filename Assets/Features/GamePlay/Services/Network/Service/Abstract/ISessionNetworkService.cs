using System;
using Ragon.Client;

namespace GamePlay.Services.Network.Service.Abstract
{
    public interface ISessionNetworkService
    {
        event Action Created;

        void Inject(RagonEntity entity);
    }
}