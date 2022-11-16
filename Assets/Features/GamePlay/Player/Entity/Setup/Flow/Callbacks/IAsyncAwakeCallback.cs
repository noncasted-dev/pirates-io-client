#region

using Cysharp.Threading.Tasks;

#endregion

namespace GamePlay.Player.Entity.Setup.Flow.Callbacks
{
    public interface IAsyncAwakeCallback
    {
        UniTask OnAsyncAwake();
    }
}