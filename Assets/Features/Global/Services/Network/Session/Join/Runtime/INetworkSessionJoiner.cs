#region

using Cysharp.Threading.Tasks;

#endregion

namespace Global.Services.Network.Session.Join.Runtime
{
    public interface INetworkSessionJoiner
    {
        UniTask<NetworkSessionJoinResultType> JoinRandom();
    }
}