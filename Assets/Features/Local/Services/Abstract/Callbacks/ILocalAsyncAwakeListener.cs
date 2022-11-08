using Cysharp.Threading.Tasks;

namespace Local.Services.Abstract.Callbacks
{
    public interface ILocalAsyncAwakeListener
    {
        UniTask OnAwakeAsync();
    }
}