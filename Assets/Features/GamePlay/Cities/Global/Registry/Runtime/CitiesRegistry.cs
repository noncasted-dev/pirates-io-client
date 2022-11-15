using GamePlay.Cities.Instance.Root.Runtime;
using UnityEngine;

namespace GamePlay.Cities.Global.Registry.Runtime
{
    public class CitiesRegistry : MonoBehaviour, ICitiesRegistry
    {
        [SerializeField] private CitiesDictionary _cities;
        
        public ICity GetCity(CityDefinition definition)
        {
            var faction = definition.Faction;

            var cities = _cities[faction];
            var city = cities[definition];

            return city;
        }
    }
}