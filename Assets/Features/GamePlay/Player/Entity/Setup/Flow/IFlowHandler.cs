using Cysharp.Threading.Tasks;

namespace GamePlay.Player.Entity.Setup.Flow
{
    public interface IFlowHandler
    {
        void InvokeAwake();
        UniTask InvokeAsyncAwake();
        void InvokeStart();
        void InvokeEnable();
        void InvokeDisable();
        void InvokeDestroy();
    }
}