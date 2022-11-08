using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Services.PlayerSpawn.SpawnPoints;
using Local.Services.DependenciesResolve;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Level.Environment.Bootstrap
{
    public class EnvironmentBootstrapper : MonoBehaviour, ILevelBootstrapper, IDependencyRegister
    {
        [SerializeField] private SceneObjectsHandler _sceneObjects;
        [SerializeField] private PlayerSpawnPoints _playerSpawnPoints;

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterComponent(_sceneObjects).As<ISceneObjectsHandler>();
            builder.RegisterComponent(_playerSpawnPoints).As<ISpawnPoints>();
        }
    }
}