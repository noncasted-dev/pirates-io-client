using GamePlay.Common.Paths;
using GamePlay.Factions.Common;
using UnityEngine;

namespace GamePlay.Cities.Instance.Root.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.CityPrefix, menuName = GamePlayAssetsPaths.CityDefinition)]
    public class CityDefinition : ScriptableObject
    {
        [SerializeField] private FactionType _faction;
        [SerializeField] private string _name;

        public FactionType Faction => _faction;
        public string Name => _name;
    }
}