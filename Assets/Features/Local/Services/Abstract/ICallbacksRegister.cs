namespace Local.Services.Abstract
{
    public interface ICallbacksRegister
    {
        void ListenLoopCallbacks(object service);
        void ListenContainerCallbacks(object service);
    }
}