using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Root;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.PlayerSpawn.Factory.Logs;
using GamePlay.Services.PlayerSpawn.SpawnPoints;
using Global.Services.AssetsFlow.Runtime.Abstract;
using Global.Services.Network.Instantiators.Runtime;
using Global.Services.Profiles.Storage;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    public class PlayerFactory : MonoBehaviour, IPlayerFactory
    {
        [Inject]
        private void Construct(
            IAssetInstantiatorFactory instantiatorFactory,
            INetworkInstantiator networkInstantiator,
            LevelScope scope,
            ISpawnPoints spawnPoints,
            IProfileStorageProvider profileStorageProvider,
            PlayerFactoryLogger logger)
        {
            _profileStorageProvider = profileStorageProvider;
            _networkInstantiator = networkInstantiator;
            _logger = logger;
            _spawnPoints = spawnPoints;
            _instantiator = instantiatorFactory.Create<GameObject>(_prefab);
            _scope = scope;
        }

        [SerializeField] private AssetReference _prefab;
        [SerializeField] private GameObject _networkPrefab;

        private IAssetInstantiator<GameObject> _instantiator;
        private PlayerFactoryLogger _logger;
        private INetworkInstantiator _networkInstantiator;
        private IProfileStorageProvider _profileStorageProvider;
        private LevelScope _scope;
        private ISpawnPoints _spawnPoints;

        public async UniTask<IPlayerRoot> Create()
        {
            var spawnPoint = _spawnPoints.GetSpawnPoint();

            var payload = new PlayerPayload(_profileStorageProvider.UserName);

            var networkObject = await _networkInstantiator.Instantiate<PlayerNetworkRoot, PlayerPayload>(
                _networkPrefab,
                spawnPoint,
                payload);

            var playerObject = await _instantiator.InstantiateAsync(Vector2.zero);
            playerObject.name = "Player";

            var playerTransform = playerObject.transform;
            playerTransform.parent = networkObject.transform;
            playerTransform.localPosition = Vector3.zero;

            _logger.OnInstantiated(spawnPoint);

            var bootstrapper = playerObject.GetComponent<IPlayerBootstrapper>();

            await bootstrapper.Bootstrap(_scope);

            var root = playerObject.GetComponent<IPlayerRoot>();

            return root;
        }
    }
}