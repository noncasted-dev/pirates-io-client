using Cysharp.Threading.Tasks;

namespace Local.Services.Abstract.Callbacks
{
    public interface ILocalAsyncBootstrappedListener
    {
        UniTask OnBootstrappedAsync();
    }
}