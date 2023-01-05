using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Handling.Result;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;

namespace Common.Local.ComposedSceneConfig
{
    public class ComposedSceneLoader : ISceneLoader
    {
        public ComposedSceneLoader(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private readonly List<SceneLoadResult> _results = new();
        private readonly ISceneLoader _sceneLoader;

        public IReadOnlyList<SceneLoadResult> Results => _results;

        public async UniTask<T> Load<T>(SceneLoadData<T> scene) where T : SceneLoadResult
        {
            var result = await _sceneLoader.Load(scene);

            _results.Add(result);

            return result;
        }
    }
}