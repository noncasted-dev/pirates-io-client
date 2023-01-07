using VContainer;

namespace Common.DiContainer.Abstract
{
    public interface IInjectionBuilder
    {
        void Inject(IObjectResolver resolver);
    }
}