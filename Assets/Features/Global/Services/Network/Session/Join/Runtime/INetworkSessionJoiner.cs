using Cysharp.Threading.Tasks;

namespace Global.Services.Network.Session.Join.Runtime
{
    public interface INetworkSessionJoiner
    {
        UniTask<NetworkSessionJoinResultType> JoinRandom();
        UniTask<NetworkSessionJoinResultType> Create();
    }
}