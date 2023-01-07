using System.Collections.Generic;
using Common.Local.Services.Abstract.Callbacks;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Factions.Common;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Player.Entity.Network.Remote.Bootstrap;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Implementation.Dead;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    [DisallowMultipleComponent]
    public class RemotePlayerBuilder : MonoBehaviour,
        ILocalAwakeListener,
        ILocalBootstrappedListener
    {
        [Inject]
        private void Construct(
            IUpdater updater,
            ILogger logger,
            IProjectileReplicator replicator,
            IVfxPoolProvider vfxPoolProvider,
            RemoteBuilderConfigAsset config)
        {
            _vfxPoolProvider = vfxPoolProvider;
            _updater = updater;
            _logger = logger;
            _replicator = replicator;
            _config = config;
        }

        private static RemotePlayerBuilder _instance;

        [SerializeField] private AssetReference _hitExplosionReference;
        [SerializeField] private RemoteViewsPool _pool;

        private RemoteBuilderConfigAsset _config;

        private IObjectProvider<AnimatedVfx> _hitExplosionPool;

        private ILogger _logger;
        private IProjectileReplicator _replicator;
        private IUpdater _updater;
        private IVfxPoolProvider _vfxPoolProvider;
        public static RemotePlayerBuilder Instance => _instance;

        public List<Transform> Remotes { get; } = new();

        public void OnAwake()
        {
            _instance = this;
        }

        public void OnBootstrapped()
        {
            _hitExplosionPool = _vfxPoolProvider.GetPool<AnimatedVfx>(_hitExplosionReference);
        }

        public void Build(GameObject remotePlayer, ShipType shipType, FactionType faction)
        {
            Remotes.Add(remotePlayer.transform);
            var prefab = _config.GetShip(shipType);

            var deadShipPool
                = _vfxPoolProvider.GetPool<DeadShipVfx>(_config.GetDead(shipType));

            var viewProvider = _pool.Handler.GetPool<PlayerRemoteView>(prefab);
            var view = viewProvider.Get(Vector2.zero);

            var rootTransform = remotePlayer.transform;
            var viewTransform = view.transform;

            viewTransform.parent = rootTransform;
            viewTransform.localPosition = Vector3.zero;

            var networkRoot = rootTransform.GetComponent<PlayerNetworkRoot>();

            view.Construct(
                _logger,
                _updater,
                _replicator,
                _hitExplosionPool,
                deadShipPool,
                networkRoot, faction);
        }
    }
}