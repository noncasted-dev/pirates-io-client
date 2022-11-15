using System.Collections.Generic;
using Common.EditableScriptableObjects.Attributes;
using GamePlay.Common.Paths;
using GamePlay.Factions.Selections.Bootstrap;
using GamePlay.Level.Environment.Bootstrap;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.LevelCameras.Runtime;
using GamePlay.Services.LevelLoops.Runtime;
using GamePlay.Services.Network.Bootstrap.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime;
using GamePlay.Services.Projectiles.Bootstrap;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using GamePlay.Services.TransitionScreens.Runtime;
using GamePlay.Services.VFX.Pool.Provider;
using Local.ComposedSceneConfig;
using Local.Services.Abstract;
using UnityEngine;
using VContainer.Unity;

namespace GamePlay.Level.Config.Runtime
{
    [CreateAssetMenu(fileName = "Level", menuName = GamePlayAssetsPaths.Root + "Scene")]
    public class LevelAsset : ComposedSceneAsset
    {
        [SerializeField] private LevelScope _scopePrefab;

        [SerializeField] [EditableObject] private PlayerFactoryAsset _playerFactory;
        [SerializeField] [EditableObject] private LevelCameraAsset _levelCamera;
        [SerializeField] [EditableObject] private LevelLoopAsset _levelLoop;
        [SerializeField] [EditableObject] private LevelEnvironmentAsset _environment;
        [SerializeField] [EditableObject] private ProjectilesAsset _projectiles;
        [SerializeField] [EditableObject] private TransitionScreenAsset _transitionScreen;
        [SerializeField] [EditableObject] private NetworkSessionAsset _networkSession;
        [SerializeField] [EditableObject] private RemotePlayerBuilderAsset _remotePlayerBuilder;
        [SerializeField] [EditableObject] private VfxPoolAsset _vfxPool;
        [SerializeField] [EditableObject] private ProjectileReplicatorAsset _projectileReplicator;
        [SerializeField] [EditableObject] private PlayerPositionProviderAsset _playerPositionProvider;
        [SerializeField] [EditableObject] private FactionSelectionAsset _factionSelection;

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
                _factionSelection
            };

            return list.ToArray();
        }

        protected override LifetimeScope AssignScope()
        {
            return _scopePrefab;
        }
    }
}