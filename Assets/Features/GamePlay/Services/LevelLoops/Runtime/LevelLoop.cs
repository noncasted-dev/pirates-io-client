﻿using System;
using Common.Local.Services.Abstract.Callbacks;
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
using GamePlay.Services.Saves.Definitions;
using GamePlay.Services.TransitionScreens.Runtime;
using GamePlay.Services.TravelOverlays.Runtime;
using GamePlay.Services.Wallets.Runtime;
using Global.Services.CurrentCameras.Runtime;
using Global.Services.FilesFlow.Runtime.Abstract;
using Global.Services.ItemFactories.Runtime;
using Global.Services.MessageBrokers.Runtime;
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
            IFileLoader fileLoader,
            IFileSaver fileSaver,
            IWalletPresenter walletPresenter,
            LevelLoopLogger logger)
        {
            _walletPresenter = walletPresenter;
            _fileSaver = fileSaver;
            _fileLoader = fileLoader;
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

        private IPlayerCargoStorage _cargo;

        private ICitiesRegistry _citiesRegistry;
        private ICurrentCamera _currentCamera;

        private IDisposable _deathListener;
        private IPlayerEntityPresenter _entityPresenter;

        private IFactionSelectionLoop _factionSelection;
        private IFileLoader _fileLoader;
        private IFileSaver _fileSaver;
        private IItemFactory _itemFactory;
        private ILevelCamera _levelCamera;

        private LevelLoopLogger _logger;
        private IPlayerFactory _playerFactory;
        private IReputation _reputation;
        private ISceneObjectsHandler _sceneObjects;
        private IDisposable _shipChangeListener;
        private ITransitionScreen _transitionScreen;
        private ITravelOverlay _travelOverlay;
        private IWalletPresenter _walletPresenter;

        public void OnLoaded()
        {
            _sceneObjects.InvokeFullStartup();

            _currentCamera.SetCamera(_levelCamera.Camera);

            _logger.OnLoaded();

            if (_fileLoader.Load(out ShipSave save) == false)
            {
                Begin().Forget();
            }
            else
            {
                for (var i = 0; i < save.Items.Count; i++)
                    _cargo.Add(_itemFactory.Create(save.Items[i], save.Count[i]));

                _walletPresenter.Set(save.Money);

                ProcessRespawn(save.ShipType, save.LastCity).Forget();
            }
        }

        public void OnEnabled()
        {
            _deathListener = Msg.Listen<PlayerDeathEvent>(OnPlayerDeath);
            _shipChangeListener = Msg.Listen<ShipChangeEvent>(OnShipChanged);
        }

        public void OnDisabled()
        {
            _deathListener?.Dispose();
            _shipChangeListener?.Dispose();
        }

        private async UniTask Begin()
        {
            var cannons = _itemFactory.Create(ItemType.Cannon, 3);
            var ball = _itemFactory.Create(ItemType.CannonBall, 90);
            var shrapnel = _itemFactory.Create(ItemType.CannonKnuppel, 30);
            var knuppel = _itemFactory.Create(ItemType.CannonShrapnel, 30);

            _cargo.Add(cannons);
            _cargo.Add(ball);
            _cargo.Add(shrapnel);
            _cargo.Add(knuppel);

            var selectedCity = await _factionSelection.SelectAsync();
            _transitionScreen.ToPlayerRespawn();

            var cityInstance = _citiesRegistry.GetCity(selectedCity);
            var spawnPosition = cityInstance.SpawnPoints.GetRandom();

            _logger.OnPlayerSpawn();

            var player = await _playerFactory.Create(spawnPosition, ShipType.Boat);

            _levelCamera.Teleport(player.Transform.position);
            _levelCamera.StartFollow(player.Transform);

            await _transitionScreen.FadeOut();

            _travelOverlay.Open();

            player.Respawn();
        }

        private async UniTaskVoid ProcessRespawn(ShipType ship, CityType city)
        {
            _logger.OnPlayerSpawn();

            _transitionScreen.ToPlayerRespawn();

            var cityInstance = _citiesRegistry.GetCity(city);
            var spawnPosition = cityInstance.SpawnPoints.GetRandom();

            var player = await _playerFactory.Create(spawnPosition, ship);

            _levelCamera.Teleport(player.Transform.position);
            _levelCamera.StartFollow(player.Transform);

            await _transitionScreen.FadeOut();

            _travelOverlay.Open();

            player.Respawn();

            _cargo.UpdateState();
        }

        public void Respawn(ShipType ship)
        {
            ProcessRespawn(ship).Forget();
        }

        private void OnPlayerDeath(PlayerDeathEvent data)
        {
            _entityPresenter.DestroyPlayer();

            var save = _fileLoader.LoadOrCreate<ShipSave>();

            save.ShipType = ShipType.Boat;
            save.Items.Clear();
            save.Count.Clear();

            _fileSaver.Save(save);

            Begin().Forget();
        }

        private void OnShipChanged(ShipChangeEvent data)
        {
            var save = _fileLoader.LoadOrCreate<ShipSave>();
            save.ShipType = data.Ship;
            _fileSaver.Save(save);

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