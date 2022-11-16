#region

using Cysharp.Threading.Tasks;

#endregion

namespace Local.Services.Abstract.Callbacks
{
    public interface ILocalAsyncBootstrappedListener
    {
        UniTask OnBootstrappedAsync();
    }
}