using Cysharp.Threading.Tasks;

namespace GamePlay.Player.Entity.Setup.Flow.Callbacks
{
    public interface IAsyncAwakeCallback
    {
        UniTask OnAsyncAwake();
    }
}