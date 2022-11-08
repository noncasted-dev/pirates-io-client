using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Global.Services.AssetsFlow.Runtime
{
    public class AssetLoadResult<T>
    {
        public AssetLoadResult(AsyncOperationHandle<T> handle, AssetReference reference)
        {
            Handle = handle;
            Reference = reference;
            Key = reference.RuntimeKey;
            Instance = handle.Result;
        }

        public readonly AsyncOperationHandle<T> Handle;
        public readonly T Instance;
        public readonly object Key;
        public readonly AssetReference Reference;

        public string AssetName => Reference.Asset.name;
    }
}