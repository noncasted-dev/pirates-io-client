using System.Collections.Generic;
using GamePlay.Common.Paths;
using GamePlay.Level.Environment.Bootstrap;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.LevelCameras.Runtime;
using GamePlay.Services.LevelLoops.Runtime;
using GamePlay.Services.Network.Bootstrap.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using GamePlay.Services.Projectiles.Bootstrap;
using GamePlay.Services.TransitionScreens.Runtime;
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

        [SerializeField] private PlayerFactoryAsset _playerFactory;
        [SerializeField] private LevelCameraAsset _levelCamera;
        [SerializeField] private LevelLoopAsset _levelLoop;
        [SerializeField] private LevelEnvironmentAsset _environment;
        [SerializeField] private ProjectilesAsset _projectiles;
        [SerializeField] private TransitionScreenAsset _transitionScreen;
        [SerializeField] private NetworkSessionAsset _networkSession;

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
                _networkSession
            };

            return list.ToArray();
        }

        protected override LifetimeScope AssignScope()
        {
            return _scopePrefab;
        }
    }
}