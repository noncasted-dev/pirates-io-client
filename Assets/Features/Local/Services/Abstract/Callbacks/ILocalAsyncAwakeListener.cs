#region

using Cysharp.Threading.Tasks;

#endregion

namespace Local.Services.Abstract.Callbacks
{
    public interface ILocalAsyncAwakeListener
    {
        UniTask OnAwakeAsync();
    }
}