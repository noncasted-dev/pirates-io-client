using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Global.Services.Common.Abstract.Scenes
{
    public class InternalScene<T>
    {
        public InternalScene(AssetReference asset)
        {
            Asset = asset;
        }

        public readonly AssetReference Asset;

        public InternalSceneLoadResult<T> CreateLoadResult(SceneInstance scene)
        {
            var searched = Search(scene.Scene);

            return new InternalSceneLoadResult<T>(scene, searched);
        }

        private T Search(Scene scene)
        {
            var rootObjects = scene.GetRootGameObjects();
            foreach (var rootObject in rootObjects)
                if (rootObject.TryGetComponent(out T searched) == true)
                    return searched;

            throw new NullReferenceException($"Searched {typeof(T)} is not found");
        }
    }
}