#region

using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using VContainer;

#endregion

namespace GamePlay.Player.Entity.Setup.Bootstrap
{
    public interface IPlayerContainerBuilder
    {
        void OnBuild(IContainerBuilder builder);
        void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister);
    }
}