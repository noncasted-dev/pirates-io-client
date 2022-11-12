using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Network.Remote.Bootstrap;
using GamePlay.Services.Projectiles.Factory;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    public class RemoteViewFactory : IObjectFactory<PlayerRemoteView>
    {
        public RemoteViewFactory(
            AssetReference reference,
            Transform parent,
            IProjectilesPoolProvider projectiles,
            ILogger logger,
            IAssetInstantiatorFactory instantiatorFactory)
        {
            _reference = reference;
            _parent = parent;
            _projectiles = projectiles;
            _logger = logger;
            _instantiatorFactory = instantiatorFactory;
        }

        private readonly IAssetInstantiatorFactory _instantiatorFactory;
        private readonly IProjectilesPoolProvider _projectiles;
        private readonly ILogger _logger;
        private readonly Transform _parent;

        private readonly AssetReference _reference;

        private IAssetInstantiator<PlayerRemoteView> _instantiator;

        public async UniTask PreloadAsync()
        {
            _instantiator = _instantiatorFactory.Create<PlayerRemoteView>(_reference);
            await _instantiator.PreloadAsync();
        }

        public void Unload()
        {
            _instantiator.Release();
        }

        public PlayerRemoteView Create(Vector2 position, float angle = 0)
        {
            var view = _instantiator.Instantiate(position, angle, _parent);
            view.Construct(_logger, _projectiles);

            return view;
        }
    }
}