using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Player.Entity.Network.Remote.Bootstrap;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.AssetsFlow.Runtime.Abstract;
using Global.Services.Updaters.Runtime.Abstract;
using Local.Services.Abstract.Callbacks;
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
            IAssetInstantiatorFactory instantiatorFactory,
            IVfxPoolProvider vfxPoolProvider)
        {
            _vfxPoolProvider = vfxPoolProvider;
            _updater = updater;
            _logger = logger;
            _replicator = replicator;
            _instantiatorFactory = instantiatorFactory;
        }
        
        private static RemotePlayerBuilder _instance;

        [SerializeField] private RemoteViewsPool _pool;
        [SerializeField] private AssetReference _prefab;
        [SerializeField] private AssetReference _hitExplosionReference;
        
        private IAssetInstantiatorFactory _instantiatorFactory;
        private ILogger _logger;
        private IProjectileReplicator _replicator;
        private IUpdater _updater;
        private IVfxPoolProvider _vfxPoolProvider;
        private IObjectProvider<AnimatedVfx> _hitExplosionPool;

        public static RemotePlayerBuilder Instance => _instance;

        public void OnAwake()
        {
            _instance = this;
        }
        
        public void OnBootstrapped()
        {
            _hitExplosionPool = _vfxPoolProvider.GetPool<AnimatedVfx>(_hitExplosionReference);
        }

        public void Build(GameObject remotePlayer)
        {
            var viewProvider = _pool.Handler.GetPool<PlayerRemoteView>(_prefab);

            var view = viewProvider.Get(Vector2.zero);

            var rootTransform = remotePlayer.transform;
            var viewTransform = view.transform;

            viewTransform.parent = rootTransform;
            viewTransform.localPosition = Vector3.zero;

            var networkRoot = rootTransform.GetComponent<PlayerNetworkRoot>();

            view.Construct(_logger, _updater, _replicator, _hitExplosionPool, networkRoot);
        }
    }
}