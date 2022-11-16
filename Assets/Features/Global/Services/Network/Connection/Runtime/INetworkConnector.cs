using Cysharp.Threading.Tasks;

namespace Global.Services.Network.Connection.Runtime
{
    public interface INetworkConnector
    {
        UniTask<NetworkConnectResultType> Connect(string userName);
    }
}