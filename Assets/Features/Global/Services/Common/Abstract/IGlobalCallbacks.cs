using Global.Services.Common.Abstract.Callbacks;

namespace Global.Services.Common.Abstract
{
    public interface IGlobalCallbacks
    {
        void Listen(object listener);
        void AddInternalCallbackLoop(IGlobalInternalCallbackLoop callbackLoop);
    }
}