#region

using VContainer;

#endregion

namespace Local.Services.DependenciesResolve
{
    public interface IDependencyResolver
    {
        void Resolve(IObjectResolver resolver);
    }
}