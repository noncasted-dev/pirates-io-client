using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Common.Local.Services.Abstract.Callbacks;
using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Level.Environment.Chunks.OcclusionCulling.Runtime;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using Global.Services.ItemFactories.Runtime;
using NaughtyAttributes;
using UnityEngine;
using VContainer;

namespace GamePlay.Level.Environment.Bootstrap
{
    public class EnvironmentBootstrapper : MonoBehaviour, ILevelBootstrapper, ILocalContainerBuildListener,
        IDependencyResolver
    {
        [SerializeField] private SceneObjectsHandler _sceneObjects;
        [SerializeField] private CitiesRegistry _citiesRegistry;
        [SerializeField] private ChunksOcclusionCulling _chunksCulling;

        [SerializeField] private CityStorage[] _storages;
        [SerializeField] private CityPort[] _ports;

        public void Resolve(IObjectResolver resolver)
        {
            var itemFactory = resolver.Resolve<IItemFactory>();
            var cargoStorage = resolver.Resolve<IPlayerCargoStorage>();

            foreach (var target in _storages)
                target.Construct(itemFactory);

            foreach (var target in _ports)
                target.Construct(cargoStorage);
        }

        public void OnContainerBuild(IDependencyRegister builder)
        {
            builder.RegisterComponent(_sceneObjects).As<ISceneObjectsHandler>();
            builder.RegisterComponent(_citiesRegistry).As<ICitiesRegistry>();
            builder.RegisterComponent(_chunksCulling).AsSelfResolvable();
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