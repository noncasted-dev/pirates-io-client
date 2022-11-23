using Cysharp.Threading.Tasks;
using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Factions.Selections.Loops.Runtime;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Player.Entity.Setup.Root;
using GamePlay.Services.LevelCameras.Runtime;
using GamePlay.Services.LevelLoops.Logs;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using GamePlay.Services.TransitionScreens.Runtime;
using GamePlay.Services.TravelOverlays.Runtime;
using Global.Services.CurrentCameras.Runtime;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.LevelLoops.Runtime
{
    [DisallowMultipleComponent]
    public class LevelLoop : MonoBehaviour, ILocalLoadListener, ILevelLoop
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
            IPlayerPositionProvider playerPositionProvider,
            IPlayerEntityPresenter entityPresenter,
            LevelLoopLogger logger)
        {
            _entityPresenter = entityPresenter;
            _playerPositionProvider = playerPositionProvider;
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
        
        private ICitiesRegistry _citiesRegistry;

        private ICurrentCamera _currentCamera;
        private IFactionSelectionLoop _factionSelection;
        private ILevelCamera _levelCamera;
        private LevelLoopLogger _logger;
        private IPlayerRoot _player;
        private IPlayerFactory _playerFactory;
        private ISceneObjectsHandler _sceneObjects;
        private ITransitionScreen _transitionScreen;
        private ITravelOverlay _travelOverlay;
        private IPlayerPositionProvider _playerPositionProvider;
        private IPlayerEntityPresenter _entityPresenter;

        public void OnLoaded()
        {
            _sceneObjects.InvokeFullStartup();

            _currentCamera.SetCamera(_levelCamera.Camera);

            _logger.OnLoaded();

            Begin().Forget();
        }

        private async UniTask Begin()
        {
            var selectedCity = await _factionSelection.SelectAsync();
            _transitionScreen.ToPlayerRespawn();

            var cityInstance = _citiesRegistry.GetCity(selectedCity);
            var spawnPosition = cityInstance.SpawnPoints.GetRandom();

            _logger.OnPlayerSpawn();

            _player = await _playerFactory.Create(spawnPosition, ShipType.Boat);
            
            _levelCamera.Teleport(_player.Transform.position);
            _levelCamera.StartFollow(_player.Transform);

            await _transitionScreen.FadeOut();

            _travelOverlay.Open();

            _player.Respawn();
        }

        public void Respawn(ShipType ship)
        {
            ProcessRespawn(ship).Forget();
        }

        private async UniTaskVoid ProcessRespawn(ShipType ship)
        {
            _entityPresenter.DestroyPlayer();
            
            _logger.OnPlayerSpawn();

            _player = await _playerFactory.Create(_playerPositionProvider.Position, ship);
            
            _levelCamera.Teleport(_player.Transform.position);
            _levelCamera.StartFollow(_player.Transform);

            await _transitionScreen.FadeOut();

            _player.Respawn();
        }
    }
}