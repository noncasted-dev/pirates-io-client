using GamePlay.Player.Entity.Network.Remote.Bootstrap;
using GamePlay.Services.Projectiles.Factory;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    [DisallowMultipleComponent]
    public class RemotePlayerBuilder : MonoBehaviour, ILocalAwakeListener
    {
        [Inject]
        private void Construct(IProjectilesPoolProvider projectiles, ILogger logger)
        {
            _logger = logger;
            _projectiles = projectiles;
        }
        
        private static RemotePlayerBuilder _instance;

        public static RemotePlayerBuilder Instance => _instance;

        [SerializeField] private RemoteViewsPool _pool;
        [SerializeField] private AssetReference _prefab;
        
        private IProjectilesPoolProvider _projectiles;
        private ILogger _logger;

        public void OnAwake()
        {
            _instance = this;   
        }

        public void Build(GameObject remotePlayer)
        {
            var viewProvider = _pool.Handler.GetPool<PlayerRemoteView>(_prefab);
            
            var view = viewProvider.Get(Vector2.zero);

            var rootTransform = remotePlayer.transform;
            var viewTransform = view.transform;

            viewTransform.parent = rootTransform;
            viewTransform.localPosition = Vector3.zero;
            
            view.Construct(_logger, _projectiles);
        }
    }
}