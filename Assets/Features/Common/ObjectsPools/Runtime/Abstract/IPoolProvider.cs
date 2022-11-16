#region

using UnityEngine.AddressableAssets;

#endregion

namespace Common.ObjectsPools.Runtime.Abstract
{
    public interface IPoolProvider
    {
        IObjectProvider<T> GetPool<T>(AssetReference reference);
    }
}