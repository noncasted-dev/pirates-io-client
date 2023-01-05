using Cysharp.Threading.Tasks;

namespace Global.Services.Common.Abstract.Scenes
{
    public interface IGlobalSceneLoader
    {
        UniTask<InternalSceneLoadResult<T>> Load<T>(InternalScene<T> scene);
    }
}