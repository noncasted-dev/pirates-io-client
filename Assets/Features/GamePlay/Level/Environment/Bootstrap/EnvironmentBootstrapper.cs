using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Level.Environment.Chunks.OcclusionCulling.Runtime;
using GamePlay.Services.LevelLoops.Runtime;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using Global.Services.ItemFactories.Runtime;
using Local.Services.DependenciesResolve;
using NaughtyAttributes;
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

        [SerializeField] private CityStorage[] _storages;
        [SerializeField] private CityPort[] _ports;

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterComponent(_sceneObjects).As<ISceneObjectsHandler>();
            builder.RegisterComponent(_citiesRegistry).As<ICitiesRegistry>();
            builder.RegisterComponent(_chunksCulling);
        }

        public void Resolve(IObjectResolver resolver)
        {
            resolver.Resolve<ChunksOcclusionCulling>();
            var itemFactory = resolver.Resolve<IItemFactory>();
            var cargoStorage = resolver.Resolve<IPlayerCargoStorage>();
            
            foreach (var target in _storages)
                target.Construct(itemFactory);
            
            foreach (var target in _ports)
                target.Construct(cargoStorage);
        }

        [Button("ScanStorages")]
        private void ScanStorages()
        {
            _storages = FindObjectsOfType<CityStorage>();
        }
        
        [Button("ScanPorts")]
        private void ScanPorts()
        {
            _ports = FindObjectsOfType<CityPort>();
        }
    }
}