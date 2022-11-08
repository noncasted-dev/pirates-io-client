using UnityEngine.AddressableAssets;

namespace Common.ObjectsPools.Runtime.Abstract
{
    public interface IPoolProvider
    {
        IObjectProvider<T> GetPool<T>(AssetReference reference) where T : class;
    }
}