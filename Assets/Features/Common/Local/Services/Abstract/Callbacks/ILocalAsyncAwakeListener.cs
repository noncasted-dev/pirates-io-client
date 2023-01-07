using Cysharp.Threading.Tasks;

namespace Common.Local.Services.Abstract.Callbacks
{
    public interface ILocalAsyncAwakeListener
    {
        UniTask OnAwakeAsync();
    }
}