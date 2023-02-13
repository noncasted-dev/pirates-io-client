using Cysharp.Threading.Tasks;
using GamePlay.Common.Areas.Common.Runtime;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Setup.Abstract;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Logs;
using GamePlay.Services.Reputation.Runtime;
using Global.Services.AssetsFlow.Runtime.Abstract;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Network.Instantiators.Runtime;
using Global.Services.Profiles.Storage;
using Ragon.Client;
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
            PlayerFactoryConfigAsset configAsset,
            PlayerFactoryLogger logger,
            IReputation reputation)
        {
            _reputation = reputation;
            _instantiatorFactory = instantiatorFactory;
            _entityPresenter = entityPresenter;
            _profileStorageProvider = profileStorageProvider;
            _networkInstantiator = networkInstantiator;
            _logger = logger;
            _scope = scope;
            _configAsset = configAsset;
        }

        private readonly BotNames _botNames = new();
        
        private PlayerFactoryConfigAsset _configAsset;
        private IPlayerEntityPresenter _entityPresenter;
        private IAssetInstantiatorFactory _instantiatorFactory;
        private PlayerFactoryLogger _logger;

        private INetworkInstantiator _networkInstantiator;
        private IProfileStorageProvider _profileStorageProvider;
        private IReputation _reputation;

        private LevelScope _scope;

        public async UniTask<IPlayerRoot> Create(Vector2 position, ShipType type)
        {
            var payload = new PlayerPayload(_profileStorageProvider.UserName, type, _reputation.Faction);

            var networkObject = await _networkInstantiator.Instantiate<LocalPlayerRoot, PlayerPayload>(
                _configAsset.NetworkPrefab,
                position,
                payload);

            var prefab = _configAsset.GetShip(type);
            var instantiator = _instantiatorFactory.Create<GameObject>(prefab);
            var playerObject = await instantiator.InstantiateAsync(Vector2.zero);

            var playerTransform = playerObject.transform;
            var networkTransform = networkObject.transform;
            var entity = networkTransform.GetComponent<RagonEntity>();

            playerTransform.parent = networkTransform;
            playerTransform.localPosition = Vector3.zero;

            _logger.OnInstantiated(position);

            var bootstrapper = playerObject.GetComponent<IPlayerBootstrapper>();

            await bootstrapper.Bootstrap(_scope);

            var root = playerObject.GetComponent<IPlayerRoot>();

            var resources = playerTransform.GetComponent<IAreaInteractor>().Resources;
            _entityPresenter.AssignPlayer(entity, networkTransform, resources);

            Msg.Publish(new PlayerSpawnedEvent(type));

            return root;
        }

        public async UniTask<IPlayerRoot> CreateBot(Vector2 position, ShipType type)
        {
            var payload = new PlayerPayload(_botNames.GetRandom(), type, _reputation.Faction);

            var networkObject = await _networkInstantiator.Instantiate<LocalPlayerRoot, PlayerPayload>(
                _configAsset.NetworkPrefab,
                position,
                payload);

            var prefab = _configAsset.GetBotShip(type);
            var instantiator = _instantiatorFactory.Create<GameObject>(prefab);
            var playerObject = await instantiator.InstantiateAsync(Vector2.zero);

            var playerTransform = playerObject.transform;
            var networkTransform = networkObject.transform;

            playerTransform.parent = networkTransform;
            playerTransform.localPosition = Vector3.zero;

            _logger.OnInstantiated(position);

            var bootstrapper = playerObject.GetComponent<IPlayerBootstrapper>();

            await bootstrapper.Bootstrap(_scope);

            var root = playerObject.GetComponent<IPlayerRoot>();

            return root;
        }
    }
}