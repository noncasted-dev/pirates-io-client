#region

using Cysharp.Threading.Tasks;

#endregion

namespace Global.Services.Network.Connection.Runtime
{
    public interface INetworkConnector
    {
        UniTask<NetworkConnectResultType> Connect(string userName);
    }
}