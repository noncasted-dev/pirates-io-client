using Cysharp.Threading.Tasks;
using Global.Services.Common.Abstract.Scenes;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Global.Bootstrappers
{
    public class GlobalSceneLoader : IGlobalSceneLoader
    {
        public async UniTask<InternalSceneLoadResult<T>> Load<T>(InternalScene<T> scene)
        {
            var handle = Addressables.LoadSceneAsync(scene.Asset, LoadSceneMode.Additive);
            var task = handle.ToUniTask();

            var result = await task;

            return scene.CreateLoadResult(result);
        }
    }
}