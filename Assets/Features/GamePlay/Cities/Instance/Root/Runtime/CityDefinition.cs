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
        [SerializeField] private FactionType _faction;
        [SerializeField] private CityType _name;
        [SerializeField] private List<ItemType> _mostProduced;
        [SerializeField] private List<ItemType> _lessProduced;
        
        public FactionType Faction => _faction;
        public CityType Name => _name;
        public IReadOnlyList<ItemType> MostProduced => _mostProduced;
        public IReadOnlyList<ItemType> LessProduced => _lessProduced;

        public void Construct(FactionType faction, CityType city)
        {
            _faction = faction;
            _name = city;
        }

        public void Clear()
        {
            _mostProduced.Clear();
            _lessProduced.Clear();
        }

        public void OnGenerated(List<ItemType> most, List<ItemType> less)
        {
            _mostProduced = most;
            _lessProduced = less;
        }
    }
}