using VContainer;

namespace Local.Services.DependenciesResolve
{
    public interface IDependencyResolver
    {
        void Resolve(IObjectResolver resolver);
    }
}