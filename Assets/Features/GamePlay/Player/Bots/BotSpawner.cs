﻿using System;
using Cysharp.Threading.Tasks;
using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Player.Entity.States.Deaths.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using Global.Services.MessageBrokers.Runtime;
using NaughtyAttributes;
using Random = UnityEngine.Random;

namespace GamePlay.Player.Bots
{
    public class BotSpawner : SceneObject
    {
        private ICitiesRegistry _citiesRegistry;
        private IDisposable _deathListener;
        private IPlayerFactory _factory;

        protected override void OnEnabled()
        {
            _deathListener = Msg.Listen<BotDeathEvent>(OnBotDeath);
        }

        protected override void OnDisabled()
        {
            _deathListener?.Dispose();
        }

        private void OnBotDeath(BotDeathEvent data)
        {
            SpawnBot().Forget();
        }

        [Button("SpawnRandom")]
        private void SpawnRandom()
        {
            _citiesRegistry = FindObjectOfType<CitiesRegistry>();
            _factory = FindObjectOfType<PlayerFactory>();

            Process().Forget();
        }

        private async UniTask SpawnBot()
        {
            var city = (CityType)Random.Range(1, 29);
            var shipType = (ShipType)Random.Range(0, 4);

            var spawnPoints = _citiesRegistry.GetCity(city).SpawnPoints;
            var ship = await _factory.CreateBot(spawnPoints.GetRandom(), shipType);

            await UniTask.Yield();
            await UniTask.Yield();
            await UniTask.Yield();

            ship.Respawn();
        }

        private async UniTaskVoid Process()
        {
            for (var i = 0; i < 150; i++)
                await SpawnBot();
        }
    }
}