using System;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Factions.Common;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Cities.Global.Registry.Runtime
{
    public class CitiesRegistry : MonoBehaviour, ICitiesRegistry
    {
        [SerializeField] private CityDefinition[] _all;

        [SerializeField] private CitiesDictionary _factionCities;

        public ICity GetCity(CityDefinition definition)
        {
            var faction = definition.Faction;
            Debug.Log(definition.Name + " " + faction);
            var cities = _factionCities[faction];
            var city = cities[definition];

            return city;
        }

        public ICity GetCity(CityType type)
        {
            CityDefinition definition = null;

            foreach (var tmp in _all)
            {
                if (tmp.Name != type)
                    continue;

                definition = tmp;
                break;
            }

            var faction = definition.Faction;
            var cities = _factionCities[faction];
            var city = cities[definition];

            return city;
        }

        [Button("Scan")]
        private void Scan()
        {
            foreach (var faction in (FactionType[])Enum.GetValues(typeof(FactionType)))
                _factionCities[faction] = new FactionCitiesDictionary();

            var cities = FindObjectsOfType<CityRoot>();

            foreach (var definition in _all)
            foreach (var city in cities)
            {
                if (city.Definition != definition)
                    continue;

                var factionCities = _factionCities[definition.Faction];
                Debug.Log(definition.Name);
                factionCities.Add(definition, city);
            }
        }
    }
}