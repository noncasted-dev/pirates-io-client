using Common.DiContainer.Abstract;

namespace Common.Local.Services.Abstract.Callbacks
{
    public interface ILocalContainerBuildListener
    {
        void OnContainerBuild(IDependencyRegister builder);
    }
}