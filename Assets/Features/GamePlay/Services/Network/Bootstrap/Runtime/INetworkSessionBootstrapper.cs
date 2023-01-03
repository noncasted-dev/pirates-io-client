using Common.DiContainer.Abstract;

namespace GamePlay.Services.Network.Bootstrap.Runtime
{
    public interface INetworkSessionBootstrapper
    {
        void Bootstrap(IDependencyRegister builder);
    }
}