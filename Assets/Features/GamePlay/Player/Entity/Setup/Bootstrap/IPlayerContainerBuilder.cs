using Common.DiContainer.Abstract;

namespace GamePlay.Player.Entity.Setup.Bootstrap
{
    public interface IPlayerContainerBuilder
    {
        void OnBuild(IDependencyRegister builder);
    }
}