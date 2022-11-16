#region

using Common.ObjectsPools.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

#endregion

namespace Common.ObjectsPools.Runtime
{
    public abstract class PoolEntryAsset : ScriptableObject
    {
        [SerializeField] private AssetReference _reference;
        [SerializeField] [Min(1)] private int _startupInstances;
        [SerializeField] private string _name;
        public object Key => _reference.RuntimeKey;
        public string Name => _name;

        protected int StartupInstances => _startupInstances;
        protected AssetReference Reference => _reference;

        public abstract IObjectsPool Create(IObjectResolver resolver, Transform parent);
    }
}