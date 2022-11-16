#region

using Global.Services.ScenesFlow.Handling.Result;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

#endregion

namespace Global.Services.ScenesFlow.Handling.Data
{
    public abstract class SceneLoadData<T> where T : SceneLoadResult
    {
        public SceneLoadData(
            AssetReference asset)
        {
            Asset = asset;
        }

        public readonly AssetReference Asset;

        public abstract T CreateLoadResult(SceneInstance scene);
    }
}