using System.Collections.Generic;
using GamePlay.Cities.Instance.Spawning.Runtime;
using GamePlay.Factions.Common;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Cities.Instance.Root.Runtime
{
    [DisallowMultipleComponent]
    public class CityRoot : MonoBehaviour, ICity
    {
        [SerializeField] private CityDefinition _definition;
        [SerializeField] private CitySpawnPoints _spawnPoints;

        public FactionType Faction => _definition.Faction;

        public CityDefinition Definition => _definition;
        public ICitySpawnPoints SpawnPoints => _spawnPoints;
    }
}