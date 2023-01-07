using VContainer;

namespace Common.Local.Services.Abstract
{
    public interface IDependencyResolver
    {
        void Resolve(IObjectResolver resolver);
    }
}