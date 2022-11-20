using System.Collections.Generic;
using Common.EditableScriptableObjects.Attributes;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime;
using GamePlay.Common.Paths;
using GamePlay.Factions.Selections.Bootstrap;
using GamePlay.Level.Environment.Bootstrap;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.DroppedObjects.Bootstrap;
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
        [SerializeField] [EditableObject] private LevelEnvironmentAsset _environment;
        [SerializeField] [EditableObject] private FactionSelectionAsset _factionSelection;
        [SerializeField] [EditableObject] private LevelCameraAsset _levelCamera;
        [SerializeField] [EditableObject] private LevelLoopAsset _levelLoop;
        [SerializeField] [EditableObject] private NetworkPlayerDataAsset _networkPlayerData;
        [SerializeField] [EditableObject] private NetworkSessionAsset _networkSession;
        [SerializeField] [EditableObject] private ObjectDropperAsset _objectDropper;
        [SerializeField] [EditableObject] private PlayerCargoAsset _playerCargo;

        [SerializeField] [EditableObject] private PlayerFactoryAsset _playerFactory;
        [SerializeField] [EditableObject] private PlayerPositionProviderAsset _playerPositionProvider;
        [SerializeField] [EditableObject] private ProjectileReplicatorAsset _projectileReplicator;
        [SerializeField] [EditableObject] private ProjectilesAsset _projectiles;
        [SerializeField] [EditableObject] private RemotePlayerBuilderAsset _remotePlayerBuilder;
        [SerializeField] [EditableObject] private TransitionScreenAsset _transitionScreen;
        [SerializeField] [EditableObject] private VfxPoolAsset _vfxPool;
        [SerializeField] [EditableObject] private TravelOverlayAsset _travelOverlay;
        [SerializeField] [EditableObject] private CityPortUiAsset _cityPortUi;
        [SerializeField] [EditableObject] private WalletAsset _wallet;

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
                _wallet
            };

            return list.ToArray();
        }

        protected override LifetimeScope AssignScope()
        {
            return _scopePrefab;
        }
    }
}