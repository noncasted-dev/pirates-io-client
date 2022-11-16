using System.Collections.Generic;
using Common.EditableScriptableObjects.Attributes;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace Common.ObjectsPools.Runtime
{
    public class ObjectsPoolsHandler : MonoBehaviour, IObjectsPoolHandler, IPoolProvider
    {
        [SerializeField] [EditableObject] private PoolEntryAsset[] _entries;

        private readonly Dictionary<object, IObjectsPool> _pools = new();

        public void Setup(IObjectResolver resolver, Scene targetScene)
        {
            foreach (var entry in _entries)
            {
                var parent = CreateParent(entry.Name, targetScene);
                var objectHandler = entry.Create(resolver, parent);

                _pools.Add(entry.Key, objectHandler);
            }
        }

        public async UniTask Prepare()
        {
            var preloadTasks = new List<UniTask>();

            foreach (var objectHandler in _pools)
            {
                var task = objectHandler.Value.PreloadAsync();
                preloadTasks.Add(task);
            }

            await UniTask.WhenAll(preloadTasks);
        }

        public void InstantiateStartupInstances()
        {
            foreach (var objectHandler in _pools)
                objectHandler.Value.InstantiateStartupInstances();
        }

        public IObjectProvider<T> GetPool<T>(AssetReference reference)
        {
            if (_pools.ContainsKey(reference.RuntimeKey) == false)
            {
                Debug.LogError($"No key: {reference.Asset.name} found");
                return null;
            }

            var pool = _pools[reference.RuntimeKey];
            var provider = pool.GetProvider<T>();

            return provider;
        }

        private Transform CreateParent(string assetName, Scene targetScene)
        {
            var root = new GameObject($"{assetName} objects")
            {
                transform =
                {
                    position = Vector3.zero
                }
            };

            SceneManager.MoveGameObjectToScene(root, targetScene);

            return root.transform;
        }
    }
}