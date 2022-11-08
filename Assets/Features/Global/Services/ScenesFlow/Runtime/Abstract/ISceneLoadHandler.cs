using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Result;
using VContainer.Unity;

namespace Global.Services.ScenesFlow.Runtime.Abstract
{
    public interface ISceneLoadHandler
    {
        UniTask<SceneLoadResult[]> Load(ISceneLoader loadHandler, LifetimeScope parent);
        void Start();
    }
}