using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Items.Abstract;
using NaughtyAttributes;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    [DisallowMultipleComponent]
    public class TradingGenerator : MonoBehaviour
    {
        [SerializeField] private GeneratableTradesDictionary _trades = new();
        [SerializeField] private TradableItemDictionary _ignoredTrades = new();

        [Button("ClearStorages")]
        private void ClearStorages()
        {
            var cities = FindObjectsOfType<CityStorage>();

            foreach (var storage in cities)
                storage.Clear();
        }

        private bool IsIgnored(ItemType item)
        {
            if (item.ToString().Contains("Cannon") == true)
                return true;

            if (item.ToString().Contains("Ship") == true)
                return true;

            if (item.ToString().Contains("Team") == true)
                return true;

            if (item.ToString().Contains("Saber") == true)
                return true;

            if (item.ToString().Contains("Money") == true)
                return true;

            if (item.ToString().Contains("Fish") == true)
                return true;

            return false;
        }

        [Button("Generate trading")]
        private void GenerateTrading()
        {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            var cities = FindObjectsOfType<CityStorage>();

            foreach (var storage in cities)
            {
                storage.Clear();

                foreach (var (key, value) in _ignoredTrades)
                    storage.AddProducable(key, value);
            }

            foreach (var (item, config) in _trades)
            {
                var city = FindCity(config.City, cities);
                var others = new List<CityStorage>();
                others.AddRange(cities);
                others.Remove(city);
                GenerateTrade(item, config, city, others);
            }

            foreach (var storage in cities)
            {
                var definition = GetDefinition(storage);

                Undo.RecordObject(definition, "Assigned");

                storage.OnGenerated(definition);

                Undo.RecordObject(definition, "Assigned");
            }
            
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        private void GenerateTrade(
            ItemType itemType,
            GeneratableTradeConfig config,
            CityStorage city,
            IReadOnlyList<CityStorage> others)
        {
            others = others
                .OrderByDescending(t => Vector3.Distance(
                    t.transform.position,
                    city.transform.position)).ToList();

            var count = others.Count;

            var mainConfig = new ItemPriceConfig(
                config.MedianCost,
                config.MedianCurveHeight,
                config.MedianCount,
                config.MaxItems,
                config.ProductionSpeed, itemType);

            Undo.RecordObject(city, "Assign tradable");

            city.AddProducable(itemType, mainConfig);

            Undo.RecordObject(city, "Assign tradable");
            for (var i = 0; i < count; i++)
            {
                var progress = i / (float)count;

                var assignCount
                    = Mathf.CeilToInt(Mathf.Lerp(config.MinCount, config.MedianCount, progress));

                var assignCurveHeight =
                    Mathf.CeilToInt(Mathf.Lerp(config.MinCurveHeight, config.MedianCurveHeight, progress));

                var price = new ItemPriceConfig(
                    config.MedianCost, assignCurveHeight, assignCount,
                    config.MaxItems, config.ProductionSpeed, itemType);

                Undo.RecordObject(others[i], "Assign tradable");

                others[i].AddProducable(itemType, price);

                EditorUtility.SetDirty(others[i]);
                Undo.RecordObject(others[i], "Assign tradable");
            }
        }

        private CityStorage FindCity(CityType target, CityStorage[] all)
        {
            foreach (var cityStorage in all)
            {
                var current = GetCity(cityStorage);

                if (current == target)
                    return cityStorage;
            }

            Debug.Log(target);

            throw new ArgumentException();
        }

        private CityType GetCity(CityStorage storage)
        {
            var parent = storage.transform.parent;
            var root = parent.GetComponentInChildren<CityRoot>();
            return root.Definition.Name;
        }

        private CityDefinition GetDefinition(CityStorage storage)
        {
            var parent = storage.transform.parent;
            var root = parent.GetComponentInChildren<CityRoot>();
            return root.Definition;
        }
    }
}