#region

using Common.ObjectsPools.Runtime.Abstract;
using UnityEngine.AddressableAssets;

#endregion

namespace GamePlay.Services.Projectiles.Factory
{
    public class ProjectilesPoolProvider : IProjectilesPoolProvider
    {
        public ProjectilesPoolProvider(
            IPoolProvider poolProvider)
        {
            _poolProvider = poolProvider;
        }

        private readonly IPoolProvider _poolProvider;

        public IObjectProvider<T> GetPool<T>(AssetReference reference)
        {
            return _poolProvider.GetPool<T>(reference);
        }
    }
}