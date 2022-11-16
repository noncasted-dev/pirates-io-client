#region

using Common.ObjectsPools.Runtime.Abstract;
using UnityEngine.AddressableAssets;

#endregion

namespace GamePlay.Services.ObjectDroppers.Pool.Runtime
{
    public class DropPoolProvider : IDropPoolProvider
    {
        public DropPoolProvider(IPoolProvider provider)
        {
            _provider = provider;
        }

        private readonly IPoolProvider _provider;

        public IObjectProvider<T> GetPool<T>(AssetReference reference)
        {
            return _provider.GetPool<T>(reference);
        }
    }
}