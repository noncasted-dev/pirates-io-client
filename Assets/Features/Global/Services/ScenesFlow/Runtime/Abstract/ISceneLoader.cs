#region

using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Handling.Result;

#endregion

namespace Global.Services.ScenesFlow.Runtime.Abstract
{
    public interface ISceneLoader
    {
        UniTask<T> Load<T>(SceneLoadData<T> scene) where T : SceneLoadResult;
    }
}