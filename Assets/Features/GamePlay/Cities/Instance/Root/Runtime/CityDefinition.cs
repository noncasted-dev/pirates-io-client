using GamePlay.Common.Paths;
using GamePlay.Factions.Common;
using UnityEngine;

namespace GamePlay.Cities.Instance.Root.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.CityPrefix, menuName = GamePlayAssetsPaths.CityDefinition)]
    public class CityDefinition : ScriptableObject
    {
        [SerializeField] private FactionType _faction;
        [SerializeField] private CityType _name;

        public FactionType Faction => _faction;
        public CityType Name => _name;
    }
}