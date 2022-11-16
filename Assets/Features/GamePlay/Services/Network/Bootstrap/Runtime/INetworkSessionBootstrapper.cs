#region

using Local.Services.Abstract;

#endregion

namespace GamePlay.Services.Network.Bootstrap.Runtime
{
    public interface INetworkSessionBootstrapper
    {
        void Bootstrap(IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister);
    }
}