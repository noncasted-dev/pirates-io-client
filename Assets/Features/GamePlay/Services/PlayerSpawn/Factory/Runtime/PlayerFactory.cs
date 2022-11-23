using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Root;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Logs;
using Global.Services.AssetsFlow.Runtime.Abstract;
using Global.Services.Network.Instantiators.Runtime;
using Global.Services.Profiles.Storage;
using Ragon.Client;
using UniRx;
using UnityEngine;
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
            IPlayerEntityPresenter entityPresenter,
            IProfileStorageProvider profileStorageProvider,
            PlayerFactoryConfig config,
            PlayerFactoryLogger logger)
        {
            _instantiatorFactory = instantiatorFactory;
            _entityPresenter = entityPresenter;
            _profileStorageProvider = profileStorageProvider;
            _networkInstantiator = networkInstantiator;
            _logger = logger;
            _scope = scope;
            _config = config;
        }

        private PlayerFactoryConfig _config;

        private LevelScope _scope;
        private PlayerFactoryLogger _logger;
        
        private INetworkInstantiator _networkInstantiator;
        private IProfileStorageProvider _profileStorageProvider;
        private IPlayerEntityPresenter _entityPresenter;
        private IAssetInstantiatorFactory _instantiatorFactory;

        public async UniTask<IPlayerRoot> Create(Vector2 position, ShipType type)
        {
            var payload = new PlayerPayload(_profileStorageProvider.UserName);

            var networkObject = await _networkInstantiator.Instantiate<PlayerNetworkRoot, PlayerPayload>(
                _config.NetworkPrefab,
                position,
                payload);
            
            var instantiator = _instantiatorFactory.Create<GameObject>(_config.GetShip(type));

            var playerObject = await instantiator.InstantiateAsync(Vector2.zero);
            playerObject.name = "Player";

            var playerTransform = playerObject.transform;
            var networkTransform = networkObject.transform;
            var entity = networkTransform.GetComponent<RagonEntity>();

            playerTransform.parent = networkTransform;
            playerTransform.localPosition = Vector3.zero;

            _entityPresenter.AssignPlayer(entity, networkTransform);

            _logger.OnInstantiated(position);

            var bootstrapper = playerObject.GetComponent<IPlayerBootstrapper>();

            await bootstrapper.Bootstrap(_scope);

            var root = playerObject.GetComponent<IPlayerRoot>();

            MessageBroker.Default.Publish(new PlayerSpawnedEvent());

            return root;
        }
    }
}