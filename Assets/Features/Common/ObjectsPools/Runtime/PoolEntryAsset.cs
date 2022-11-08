using Common.ObjectsPools.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Common.ObjectsPools.Runtime
{
    public abstract class PoolEntryAsset : ScriptableObject
    {
        [SerializeField] private AssetReference _reference;
        [SerializeField] [Min(1)] private int _startupInstances;

        public object Key => _reference.RuntimeKey;
        public abstract string Name { get; }

        protected int StartupInstances => _startupInstances;
        protected AssetReference Reference => _reference;

        public abstract IObjectsPool Create(IObjectResolver resolver, Transform parent);
    }
}