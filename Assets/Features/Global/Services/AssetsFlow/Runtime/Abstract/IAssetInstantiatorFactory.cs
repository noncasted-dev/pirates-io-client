#region

using UnityEngine.AddressableAssets;

#endregion

namespace Global.Services.AssetsFlow.Runtime.Abstract
{
    public interface IAssetInstantiatorFactory
    {
        IAssetInstantiator<T> Create<T>(AssetReference reference) where T : class;
    }
}