#region

using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Root;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.Network.PlayerDataProvider.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Logs;
using Global.Services.AssetsFlow.Runtime.Abstract;
using Global.Services.Network.Instantiators.Runtime;
using Global.Services.Profiles.Storage;
using Ragon.Client;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

#endregion

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    public class PlayerFactory : MonoBehaviour, IPlayerFactory
    {
        [Inject]
        private void Construct(
            IAssetInstantiatorFactory instantiatorFactory,
            INetworkInstantiator networkInstantiator,
            LevelScope scope,
            IPlayerTransformPresenter transformPresenter,
            IProfileStorageProvider profileStorageProvider,
            INetworkPlayerDataPresenter networkPlayerDataPresenter,
            PlayerFactoryLogger logger)
        {
            _networkPlayerDataPresenter = networkPlayerDataPresenter;
            _transformPresenter = transformPresenter;
            _profileStorageProvider = profileStorageProvider;
            _networkInstantiator = networkInstantiator;
            _logger = logger;
            _instantiator = instantiatorFactory.Create<GameObject>(_prefab);
            _scope = scope;
        }

        [SerializeField] private AssetReference _prefab;
        [SerializeField] private GameObject _networkPrefab;

        private IAssetInstantiator<GameObject> _instantiator;
        private PlayerFactoryLogger _logger;
        private INetworkInstantiator _networkInstantiator;
        private INetworkPlayerDataPresenter _networkPlayerDataPresenter;
        private IProfileStorageProvider _profileStorageProvider;
        private LevelScope _scope;
        private IPlayerTransformPresenter _transformPresenter;

        public async UniTask<IPlayerRoot> Create(Vector2 position)
        {
            var payload = new PlayerPayload(_profileStorageProvider.UserName);

            var networkObject = await _networkInstantiator.Instantiate<PlayerNetworkRoot, PlayerPayload>(
                _networkPrefab,
                position,
                payload);

            var entity = networkObject.GetComponent<RagonEntity>();

            _networkPlayerDataPresenter.SetEntityId(entity.Id);

            var playerObject = await _instantiator.InstantiateAsync(Vector2.zero);
            playerObject.name = "Player";

            var playerTransform = playerObject.transform;
            var networkTransform = networkObject.transform;

            playerTransform.parent = networkTransform;
            playerTransform.localPosition = Vector3.zero;

            _transformPresenter.AssignPlayer(networkTransform);

            _logger.OnInstantiated(position);

            var bootstrapper = playerObject.GetComponent<IPlayerBootstrapper>();

            await bootstrapper.Bootstrap(_scope);

            var root = playerObject.GetComponent<IPlayerRoot>();

            return root;
        }
    }
}