using System.Collections.Generic;
using Global.Services.AssetsFlow.Logs;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace Global.Services.AssetsFlow.Runtime
{
    public class AssetsReferencesStorage : MonoBehaviour, IAssetsReferenceStorage
    {
        [Inject]
        public void Construct(AssetsFlowLogger logger)
        {
            _logger = logger;
        }

        private readonly Dictionary<string, object> _references = new();

        private AssetsFlowLogger _logger;

        public bool Contains(string key)
        {
            Debug.Log($"Check in storage: {key}");
            return _references.ContainsKey(key);
        }

        public void Add<T>(AssetLoadResult<T> result)
        {
            _logger.OnStorageAdd(result.AssetName);
            
            Debug.Log($"Add to storage: {result.Key}");
            _references.Add(result.Key, result);
        }

        public void Remove<T>(AssetLoadResult<T> result)
        {
            _logger.OnStorageRemove(result.AssetName);

            _references.Remove(result.Key);
        }

        public AssetLoadResult<T> GetResult<T>(string key)
        {
            var result = _references[key] as AssetLoadResult<T>;

            _logger.OnStorageGetResult(result.AssetName);

            return result;
        }
    }
}