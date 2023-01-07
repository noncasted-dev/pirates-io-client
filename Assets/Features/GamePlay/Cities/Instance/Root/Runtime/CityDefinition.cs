using System.Collections.Generic;
using GamePlay.Common.Paths;
using GamePlay.Factions.Common;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Cities.Instance.Root.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.CityPrefix, menuName = GamePlayAssetsPaths.CityDefinition)]
    public class CityDefinition : ScriptableObject
    {
        public void Construct(FactionType faction, CityType city)
        {
            _faction = faction;
            _name = city;
        }

        [SerializeField] private FactionType _faction;
        [SerializeField] private CityType _name;
        [SerializeField] private List<ItemType> _mostProduced;
        [SerializeField] private List<ItemType> _leastProduced;

        public FactionType Faction => _faction;
        public CityType Name => _name;
        public IReadOnlyList<ItemType> MostProduced => _mostProduced;
        public IReadOnlyList<ItemType> LeastProduced => _leastProduced;

        public void Clear()
        {
            _mostProduced.Clear();
            _leastProduced.Clear();
        }

        public void AddMost(ItemType type)
        {
            _mostProduced.Add(type);
        }

        public void AddLeast(ItemType type)
        {
            _leastProduced.Add(type);
        }
    }
}