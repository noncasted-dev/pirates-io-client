using VContainer;

namespace Common.DiContainer.Abstract
{
    public interface IDependenciesBuilder
    {
        void RegisterAll(IContainerBuilder builder);
        void ResolveAll(IObjectResolver resolver);
        void ResolveAllWithCallbacks(IObjectResolver resolver, ICallbackRegister callbackRegister);
    }
}