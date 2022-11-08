using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Root;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.PlayerSpawn.Factory.Logs;
using GamePlay.Services.PlayerSpawn.SpawnPoints;
using Global.Services.AssetsFlow.Runtime.Abstract;
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
            LevelScope scope,
            ISpawnPoints spawnPoints,
            PlayerFactoryLogger logger)
        {
            _logger = logger;
            _spawnPoints = spawnPoints;
            _instantiator = instantiatorFactory.Create<GameObject>(_prefab);
            _scope = scope;
        }

        [SerializeField] private AssetReference _prefab;

        private IAssetInstantiator<GameObject> _instantiator;
        private PlayerFactoryLogger _logger;
        private LevelScope _scope;
        private ISpawnPoints _spawnPoints;

        public async UniTask<IPlayerRoot> Create()
        {
            var spawnPoint = _spawnPoints.GetSpawnPoint();
            var playerObject = await _instantiator.InstantiateAsync(spawnPoint);
            playerObject.name = "Player";

            _logger.OnInstantiated(spawnPoint);

            var bootstrapper = playerObject.GetComponent<IPlayerBootstrapper>();

            await bootstrapper.Bootstrap(_scope);

            var root = playerObject.GetComponent<IPlayerRoot>();

            return root;
        }
    }
}