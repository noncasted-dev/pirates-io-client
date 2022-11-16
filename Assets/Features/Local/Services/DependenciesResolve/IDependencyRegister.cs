using VContainer;

namespace Local.Services.DependenciesResolve
{
    public interface IDependencyRegister
    {
        void Register(IContainerBuilder builder);
    }
}