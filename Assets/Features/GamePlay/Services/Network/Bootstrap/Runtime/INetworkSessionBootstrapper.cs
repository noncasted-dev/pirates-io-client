using Local.Services.Abstract;

namespace GamePlay.Services.Network.Bootstrap.Runtime
{
    public interface INetworkSessionBootstrapper
    {
        void Bootstrap(IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister);
    }
}