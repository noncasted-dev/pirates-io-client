#region

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Result;
using Global.Services.ScenesFlow.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

#endregion

namespace Global.Services.ScenesFlow.Runtime
{
    public class ScenesUnloader : MonoBehaviour, ISceneUnloader
    {
        [Inject]
        private void Construct(ScenesFlowLogger logger)
        {
            _logger = logger;
        }

        private ScenesFlowLogger _logger;

        public async UniTask Unload(SceneLoadResult result)
        {
            if (result == null)
                return;

            _logger.OnSceneUnload(result.Instance.Scene);

            var task = Addressables.UnloadSceneAsync(result.Instance).ToUniTask();

            await task;
        }

        public async UniTask Unload(IReadOnlyList<SceneLoadResult> scenes)
        {
            if (scenes == null || scenes.Count == 0)
                return;

            var tasks = new UniTask[scenes.Count];

            foreach (var scene in scenes)
                _logger.OnSceneUnload(scene.Instance.Scene);

            for (var i = 0; i < scenes.Count; i++)
                tasks[i] = Addressables.UnloadSceneAsync(scenes[i].Instance).ToUniTask();

            await UniTask.WhenAll(tasks);
        }
    }
}