using Cysharp.Threading.Tasks;

namespace Global.Services.Network.Session.Leave.Runtime
{
    public interface INetworkSessionLeaver
    {
        UniTask Leave();
    }
}