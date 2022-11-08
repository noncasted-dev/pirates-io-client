using System;
using Global.Services.ScenesFlow.Handling.Result;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Global.Services.ScenesFlow.Handling.Data
{
    public class TypedSceneLoadData<T> : SceneLoadData<TypedSceneLoadResult<T>>
    {
        public TypedSceneLoadData(AssetReference asset) : base(asset)
        {
        }

        public override TypedSceneLoadResult<T> CreateLoadResult(SceneInstance scene)
        {
            var searched = Search(scene.Scene);

            return new TypedSceneLoadResult<T>(scene, searched);
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