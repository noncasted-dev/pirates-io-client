using Cysharp.Threading.Tasks;
using Global.Services.AssetsFlow.Logs;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Global.Services.AssetsFlow.Runtime
{
    public class AssetLoader : MonoBehaviour, IAssetLoader
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

        public async UniTask<AssetLoadResult<T>> Load<T>(AssetReference reference)
        {
            if (_storage.Contains(reference.AssetGUID) == true)
                return _storage.GetResult<T>(reference.AssetGUID);
            
            var handle = reference.LoadAssetAsync<T>();
            var task = handle.ToUniTask();
            await task;

            if (handle.Result == null)
                _logger.OnLoadFailed(reference.Asset.name, handle.OperationException.Message);

            var result = new AssetLoadResult<T>(handle, reference);

            _logger.OnLoad(result.AssetName);

            _storage.Add(result);

            return result;
        }
    }
}