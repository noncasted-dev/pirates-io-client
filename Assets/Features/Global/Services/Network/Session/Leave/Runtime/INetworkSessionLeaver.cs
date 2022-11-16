#region

using Cysharp.Threading.Tasks;

#endregion

namespace Global.Services.Network.Session.Leave.Runtime
{
    public interface INetworkSessionLeaver
    {
        UniTask Leave();
    }
}