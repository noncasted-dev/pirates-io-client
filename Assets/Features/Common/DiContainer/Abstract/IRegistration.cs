namespace Common.DiContainer.Abstract
{
    public interface IRegistration
    {
        IRegistration AsCallbackListener();
        IRegistration AsSelf();
        IRegistration AsImplementedInterfaces();
        IRegistration AsSelfResolvable();
        IRegistration As<T>();
        IRegistration WithParameter<T>(T value);
    }
}