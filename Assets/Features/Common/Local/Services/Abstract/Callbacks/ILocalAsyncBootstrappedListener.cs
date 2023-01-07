using Cysharp.Threading.Tasks;

namespace Common.Local.Services.Abstract.Callbacks
{
    public interface ILocalAsyncBootstrappedListener
    {
        UniTask OnBootstrappedAsync();
    }
}