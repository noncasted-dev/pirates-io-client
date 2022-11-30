using System;
using Cysharp.Threading.Tasks;
using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Factions.Selections.Loops.Runtime;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Player.Entity.States.Deaths.Runtime;
using GamePlay.Services.LevelCameras.Runtime;
using GamePlay.Services.LevelLoops.Logs;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using GamePlay.Services.Reputation.Runtime;
using GamePlay.Services.TransitionScreens.Runtime;
using GamePlay.Services.TravelOverlays.Runtime;
using Global.Services.CurrentCameras.Runtime;
using Global.Services.ItemFactories.Runtime;
using Local.Services.Abstract.Callbacks;
using UniRx;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.LevelLoops.Runtime
{
    [DisallowMultipleComponent]
    public class LevelLoop :
        MonoBehaviour,
        ILocalLoadListener,
        ILocalSwitchListener
    {
        [Inject]
        private void Construct(
            ISceneObjectsHandler sceneObjects,
            IPlayerFactory playerFactory,
            ICurrentCamera currentCamera,
            ILevelCamera levelCamera,
            ITransitionScreen transitionScreen,
            IFactionSelectionLoop factionSelection,
            ICitiesRegistry citiesRegistry,
            ITravelOverlay travelOverlay,
            IItemFactory itemFactory,
            IPlayerCargoStorage cargo,
            IPlayerEntityPresenter entityPresenter,
            IReputation reputation,
            LevelLoopLogger logger)
        {
            _reputation = reputation;
            _cargo = cargo;
            _itemFactory = itemFactory;
            _entityPresenter = entityPresenter;
            _travelOverlay = travelOverlay;
            _citiesRegistry = citiesRegistry;
            _factionSelection = factionSelection;
            _logger = logger;
            _transitionScreen = transitionScreen;
            _sceneObjects = sceneObjects;
            _playerFactory = playerFactory;
            _currentCamera = currentCamera;
            _levelCamera = levelCamera;
        }

        private IDisposable _deathListener;
        private IDisposable _shipChangeListener;
        
        private ICitiesRegistry _citiesRegistry;
        private ICurrentCamera _currentCamera;
        private ILevelCamera _levelCamera;
        
        private IFactionSelectionLoop _factionSelection;

        private LevelLoopLogger _logger;
        private IPlayerFactory _playerFactory;
        private ISceneObjectsHandler _sceneObjects;
        private ITransitionScreen _transitionScreen;
        private ITravelOverlay _travelOverlay;
        private IPlayerEntityPresenter _entityPresenter;
        private IItemFactory _itemFactory;
        private IPlayerCargoStorage _cargo;
        private IReputation _reputation;

        public void OnEnabled()
        {
            _deathListener = MessageBroker.Default.Receive<PlayerDeathEvent>().Subscribe(OnPlayerDeath);
            _shipChangeListener = MessageBroker.Default.Receive<ShipChangeEvent>().Subscribe(OnShipChanged);
        }

        public void OnDisabled()
        {
            _deathListener?.Dispose();
            _shipChangeListener?.Dispose();
        }

        public void OnLoaded()
        {
            Debug.Log(17);

            _sceneObjects.InvokeFullStartup();

            Debug.Log(18);

            
            _currentCamera.SetCamera(_levelCamera.Camera);

            Debug.Log(19);

            
            _logger.OnLoaded();

            Debug.Log(20);

            Begin().Forget();
        }

        private async UniTask Begin()
        {
            var cannons = _itemFactory.Create(ItemType.Cannon, 3);
            var ball = _itemFactory.Create(ItemType.CannonBall, 90);
            var shrapnel = _itemFactory.Create(ItemType.CannonKnuppel, 30);
            var knuppel = _itemFactory.Create(ItemType.CannonShrapnel, 30);
            Debug.Log(21);

            
            _cargo.Add(cannons);
            _cargo.Add(ball);
            _cargo.Add(shrapnel);
            _cargo.Add(knuppel);
            
            Debug.Log(22);

            
            var selectedCity = await _factionSelection.SelectAsync();
            _transitionScreen.ToPlayerRespawn();

            var cityInstance = _citiesRegistry.GetCity(selectedCity);
            var spawnPosition = cityInstance.SpawnPoints.GetRandom();
            
            _logger.OnPlayerSpawn();
            
            Debug.Log(23);


            var player = await _playerFactory.Create(spawnPosition, ShipType.Boat);

            _levelCamera.Teleport(player.Transform.position);
            _levelCamera.StartFollow(player.Transform);
            
            Debug.Log(24);


            await _transitionScreen.FadeOut();

            _travelOverlay.Open();

            player.Respawn();
            
            Debug.Log(25);

        }

        public void Respawn(ShipType ship)
        {
            ProcessRespawn(ship).Forget();
        }

        private void OnPlayerDeath(PlayerDeathEvent data)
        {
            _entityPresenter.DestroyPlayer();

            Begin().Forget();
        }
        
        private void OnShipChanged(ShipChangeEvent data)
        {
            ProcessRespawn(data.Ship).Forget();
        }

        private async UniTaskVoid ProcessRespawn(ShipType ship)
        {
            if (ship == ShipType.Boat)
            {
                var cannons = _itemFactory.Create(ItemType.Cannon, 3);
                _cargo.Add(cannons);
            }

            _entityPresenter.DestroyPlayer();

            _logger.OnPlayerSpawn();

            var cityInstance = _citiesRegistry.GetCity(_reputation.LastCity);
            var spawnPosition = cityInstance.SpawnPoints.GetRandom();
            var player = await _playerFactory.Create(spawnPosition, ship);

            _levelCamera.Teleport(player.Transform.position);
            _levelCamera.StartFollow(player.Transform);

            await _transitionScreen.FadeOut();

            player.Respawn();
            
            _cargo.UpdateState();
        }
    }
}