using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Common.SceneObjects.Runtime;
using Local.Services.DependenciesResolve;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Level.Environment.Bootstrap
{
    public class EnvironmentBootstrapper : MonoBehaviour, ILevelBootstrapper, IDependencyRegister
    {
        [SerializeField] private SceneObjectsHandler _sceneObjects;
        [SerializeField] private CitiesRegistry _citiesRegistry;

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterComponent(_sceneObjects).As<ISceneObjectsHandler>();
            builder.RegisterComponent(_citiesRegistry).As<ICitiesRegistry>();
        }
    }
}