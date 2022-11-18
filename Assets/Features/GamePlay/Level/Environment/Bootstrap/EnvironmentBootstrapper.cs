using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Level.Environment.Chunks.OcclusionCulling.Runtime;
using Local.Services.DependenciesResolve;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Level.Environment.Bootstrap
{
    public class EnvironmentBootstrapper : MonoBehaviour, ILevelBootstrapper, IDependencyRegister, IDependencyResolver
    {
        [SerializeField] private SceneObjectsHandler _sceneObjects;
        [SerializeField] private CitiesRegistry _citiesRegistry;
        [SerializeField] private ChunksOcclusionCulling _chunksCulling;

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterComponent(_sceneObjects).As<ISceneObjectsHandler>();
            builder.RegisterComponent(_citiesRegistry).As<ICitiesRegistry>();
            builder.RegisterComponent(_chunksCulling);
        }

        public void Resolve(IObjectResolver resolver)
        {
            resolver.Resolve<ChunksOcclusionCulling>();
        }
    }
}