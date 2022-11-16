#region

using Common.EditableScriptableObjects.Attributes;
using GamePlay.Cities.Instance.Spawning.Runtime;
using GamePlay.Factions.Common;
using UnityEngine;

#endregion

namespace GamePlay.Cities.Instance.Root.Runtime
{
    [DisallowMultipleComponent]
    public class CityRoot : MonoBehaviour, ICity
    {
        [SerializeField] [EditableObject] private CityDefinition _definition;
        [SerializeField] private CitySpawnPoints _spawnPoints;
        public FactionType Faction => _definition.Faction;

        public CityDefinition Definition => _definition;
        public ICitySpawnPoints SpawnPoints => _spawnPoints;
    }
}