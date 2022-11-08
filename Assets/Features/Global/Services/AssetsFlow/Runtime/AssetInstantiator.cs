using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Global.Services.AssetsFlow.Logs;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Global.Services.AssetsFlow.Runtime
{
    public class AssetInstantiator<T> : IAssetInstantiator<T> where T : class
    {
        public AssetInstantiator(
            IAssetLoader loader,
            IAssetUnloader unloader,
            AssetsFlowLogger logger,
            AssetReference reference)
        {
            _loader = loader;
            _unloader = unloader;
            _logger = logger;
            _reference = reference;

            if (typeof(T) == typeof(GameObject))
                IsTypeGameObject = true;
        }

        private readonly LinkedList<GameObject> _instances = new();

        private readonly IAssetLoader _loader;
        private readonly AssetsFlowLogger _logger;
        private readonly AssetReference _reference;
        private readonly IAssetUnloader _unloader;
        private readonly bool IsTypeGameObject;

        private bool _isLoading;

        private AssetLoadResult<GameObject> _result;

        public async UniTask PreloadAsync()
        {
            if (_result != null)
            {
                _logger.OnAlreadyPreloaded(_reference.Asset.name);
                return;
            }

            if (_isLoading == true)
            {
                await UniTask.WaitUntil(() => _result != null);
                return;
            }

            _isLoading = true;

            _result = await _loader.Load<GameObject>(_reference);
            _logger.OnPreloaded(_result.AssetName);
        }

        public async UniTask<T> InstantiateAsync(Vector2 position)
        {
            await PreloadAsync();

            _logger.OnInstantiate(_result.AssetName);

            var gameObject = Object.Instantiate(_result.Instance, position, Quaternion.identity);

            _instances.AddLast(gameObject);

            if (IsTypeGameObject == true)
                return gameObject as T;

            return gameObject.GetComponent<T>();
        }

        public T Instantiate(Vector2 position, float angle = 0f, Transform parent = null)
        {
            if (_result == null)
            {
                _logger.OnInstantiateFailed(_reference.Asset.name);
                return null;
            }

            _logger.OnInstantiate(_result.AssetName);

            var gameObject = Object.Instantiate(
                _result.Instance,
                position,
                Quaternion.Euler(0f, 0f, angle));

            if (parent != null)
                gameObject.transform.parent = parent;

            _instances.AddLast(gameObject);

            if (IsTypeGameObject == true)
                return gameObject as T;

            return gameObject.GetComponent<T>();
        }

        public void Release()
        {
            if (_result == null)
                return;

            foreach (var instance in _instances)
            {
                if (instance == null)
                    continue;

                Object.Destroy(instance);
            }

            _logger.OnRelease(_result.AssetName);

            _unloader.Unload(_result);
            _result = null;
        }

        public void Preload()
        {
            PreloadAsync().Forget();
        }
    }
}