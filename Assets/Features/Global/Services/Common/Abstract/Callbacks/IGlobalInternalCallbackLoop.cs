using Cysharp.Threading.Tasks;

namespace Global.Services.Common.Abstract.Callbacks
{
    public interface IGlobalInternalCallbackLoop
    {
        UniTask Run();
    }
}