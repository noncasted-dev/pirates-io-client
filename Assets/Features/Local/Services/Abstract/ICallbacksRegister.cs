namespace Local.Services.Abstract
{
    public interface ICallbacksRegister
    {
        void ListenFlowCallbacks(object service);
        void ListenContainerCallbacks(object service);
    }
}