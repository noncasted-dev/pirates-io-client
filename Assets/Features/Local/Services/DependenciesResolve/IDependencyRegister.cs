#region

using VContainer;

#endregion

namespace Local.Services.DependenciesResolve
{
    public interface IDependencyRegister
    {
        void Register(IContainerBuilder builder);
    }
}