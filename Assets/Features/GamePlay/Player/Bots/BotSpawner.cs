using System;
using Cysharp.Threading.Tasks;
using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace GamePlay.Player.Bots
{
    public class BotSpawner : MonoBehaviour
    {
        private IDisposable _deathListener;
        private IPlayerFactory _factory;
        private ICitiesRegistry _citiesRegistry;

        private void OnEnable()
        {
            _deathListener = MessageBroker.Default.Receive<BotDeathEvent>().Subscribe(OnBotDeath);
        }

        private void OnDisable()
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