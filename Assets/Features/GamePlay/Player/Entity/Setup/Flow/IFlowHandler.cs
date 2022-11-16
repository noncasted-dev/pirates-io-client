#region

using Cysharp.Threading.Tasks;

#endregion

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