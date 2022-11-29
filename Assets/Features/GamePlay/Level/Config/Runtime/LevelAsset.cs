using System.Collections.Generic;
using Features.GamePlay.Services.Maps.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime;
using GamePlay.Common.Paths;
using GamePlay.Factions.Selections.Bootstrap;
using GamePlay.Level.Environment.Bootstrap;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.DroppedObjects.Bootstrap;
using GamePlay.Services.FishSpawn.Runtime;
using GamePlay.Services.LevelCameras.Runtime;
using GamePlay.Services.LevelLoops.Runtime;
using GamePlay.Services.Network.Bootstrap.Runtime;
using GamePlay.Services.Network.PlayerDataProvider.Runtime;
using GamePlay.Services.PlayerCargos.Bootstrap;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime;
using GamePlay.Services.Projectiles.Bootstrap;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using GamePlay.Services.Reputation.Runtime;
using GamePlay.Services.TransitionScreens.Runtime;
using GamePlay.Services.TravelOverlays.Runtime;
using GamePlay.Services.VFX.Pool.Provider;
using GamePlay.Services.Wallets.Runtime;
using Local.ComposedSceneConfig;
using Local.Services.Abstract;
using UnityEngine;
using VContainer.Unity;

namespace GamePlay.Level.Config.Runtime
{
    [CreateAssetMenu(fileName = "Level", menuName = GamePlayAssetsPaths.Root + "Scene")]
    public class LevelAsset : ComposedSceneAsset
    {
        [SerializeField]  private LevelEnvironmentAsset _environment;
        [SerializeField]  private FactionSelectionAsset _factionSelection;
        [SerializeField]  private LevelCameraAsset _levelCamera;
        [SerializeField]  private LevelLoopAsset _levelLoop;
        [SerializeField]  private NetworkPlayerDataAsset _networkPlayerData;
        [SerializeField]  private NetworkSessionAsset _networkSession;
        [SerializeField]  private ObjectDropperAsset _objectDropper;
        [SerializeField]  private PlayerCargoAsset _playerCargo;

        [SerializeField]  private PlayerFactoryAsset _playerFactory;
        [SerializeField]  private PlayerPositionProviderAsset _playerPositionProvider;
        [SerializeField]  private ProjectileReplicatorAsset _projectileReplicator;
        [SerializeField]  private ProjectilesAsset _projectiles;
        [SerializeField]  private RemotePlayerBuilderAsset _remotePlayerBuilder;
        [SerializeField]  private TransitionScreenAsset _transitionScreen;
        [SerializeField]  private VfxPoolAsset _vfxPool;
        [SerializeField]  private TravelOverlayAsset _travelOverlay;
        [SerializeField]  private CityPortUiAsset _cityPortUi;
        [SerializeField]  private WalletAsset _wallet;
        [SerializeField]  private ReputationAsset _reputation;
        [SerializeField]  private MapAsset _map;
        [SerializeField]  private FishSpawnerAsset _fishSpawner;

        [SerializeField] private LevelScope _scopePrefab;

        protected override LocalServiceAsset[] AssignServices()
        {
            var list = new List<LocalServiceAsset>
            {
                _playerFactory,
                _levelCamera,
                _levelLoop,
                _environment,
                _projectiles,
                _transitionScreen,
                _networkSession,
                _remotePlayerBuilder,
                _vfxPool,
                _projectileReplicator,
                _playerPositionProvider,
                _factionSelection,
                _networkPlayerData,
                _objectDropper,
                _playerCargo,
                _travelOverlay,
                _cityPortUi,
                _wallet,
                _reputation,
                _map,
                _fishSpawner
            };

            return list.ToArray();
        }

        protected override LifetimeScope AssignScope()
        {
            return _scopePrefab;
        }
    }
}