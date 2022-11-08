using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Handling.Result;
using Global.Services.ScenesFlow.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace Global.Services.ScenesFlow.Runtime
{
    public class ScenesLoader : MonoBehaviour, ISceneLoader
    {
        [Inject]
        private void Construct(ScenesFlowLogger logger)
        {
            _logger = logger;
        }

        private ScenesFlowLogger _logger;

        public async UniTask<T> Load<T>(SceneLoadData<T> scene) where T : SceneLoadResult
        {
            var handle = Addressables.LoadSceneAsync(scene.Asset, LoadSceneMode.Additive);
            var task = handle.ToUniTask();

            var result = await task;

            _logger.OnSceneLoad(result.Scene);

            return scene.CreateLoadResult(result);
        }
    }
}