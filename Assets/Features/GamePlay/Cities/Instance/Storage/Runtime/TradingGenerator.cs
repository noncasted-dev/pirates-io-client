using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Items.Abstract;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    [DisallowMultipleComponent]
    public class TradingGenerator : MonoBehaviour
    {
        [SerializeField] private GeneratableTradesDictionary _trades = new();
        [SerializeField] private TradableItemDictionary _ignoredTrades = new();

        [Button("Generate trading")]
        private void GenerateTrading()
        {
            var cities = FindObjectsOfType<CityStorage>();

            foreach (var (item, config) in _trades)
            {
                var city = FindCity(config.City, cities);
                var others = new List<CityStorage>();
                others.AddRange(cities);
                GenerateTrade(item, config, city, others);
            }
        }

        private Dictionary<CityType, TradableItemDictionary> GenerateTrade(
            ItemType mainType,
            GeneratableTradeConfig config,
            CityStorage city,
            IReadOnlyList<CityStorage> others)
        {
            others = others
                .OrderBy(t => Vector3.Distance(
                    t.transform.position,
                    city.transform.position)).ToList();

            var count = others.Count;

            var prices = new TradableItemDictionary();

            var mainConfig = new ItemPriceConfig(
                config.MedianCost,
                config.MedianCurveHeight,
                config.MedianCount,
                config.MedianMaxItems,
                config.ProductionSpeed);

            foreach (var (item, tradeConfig) in _trades)
            {
                for (var i = 0; i < count; i++)
                {
                    var progress = i / (float)count;

                    var assignCount
                        = Mathf.CeilToInt(Mathf.Lerp(config.MedianCount, config.MinCount, progress));
                    
                    var assignCurveHeight =
                        Mathf.CeilToInt(Mathf.Lerp(config.MedianCurveHeight, config.MedianCurveHeight, progress));

                    var price = new ItemPriceConfig(
                        config.MedianCost, assignCurveHeight, assignCount,
                        config.MedianMaxItems, config.ProductionSpeed);
                }
            }

            return null;
        }

        private CityStorage FindCity(CityType target, CityStorage[] all)
        {
            foreach (var cityStorage in all)
            {
                var current = GetCity(cityStorage);

                if (current == target)
                    return cityStorage;
            }

            throw new ArgumentException();
        }

        private CityType GetCity(CityStorage storage)
        {
            var parent = storage.transform.parent;
            var root = parent.GetComponentInChildren<CityRoot>();
            return root.Definition.Name;
        }
    }
}