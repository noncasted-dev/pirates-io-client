using Global.Services.AssetsFlow.Logs;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Global.Services.AssetsFlow.Runtime
{
    public class AssetUnloader : MonoBehaviour, IAssetUnloader
    {
        [Inject]
        public void Construct(
            IAssetsReferenceStorage storage,
            AssetsFlowLogger logger)
        {
            _storage = storage;
            _logger = logger;
        }

        private AssetsFlowLogger _logger;

        private IAssetsReferenceStorage _storage;

        public void Unload<T>(AssetLoadResult<T> result)
        {
            _logger.OnUnload(result.AssetName);

            _storage.Remove(result);
            Addressables.Release(result.Handle);
        }
    }
}