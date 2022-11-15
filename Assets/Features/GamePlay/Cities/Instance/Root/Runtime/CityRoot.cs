using GamePlay.Factions.Common;
using UnityEngine;

namespace GamePlay.Cities.Instance.Root.Runtime
{
    [DisallowMultipleComponent]
    public class CityRoot : MonoBehaviour
    {
        [SerializeField] private FactionType _faction;

        public FactionType Faction => _faction;
    }
}