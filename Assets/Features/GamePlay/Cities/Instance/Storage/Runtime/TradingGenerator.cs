using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Items.Abstract;
using NaughtyAttributes;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
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
#if UNITY_EDITOR
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
#endif
            var cities = FindObjectsOfType<CityStorage>().ToList();

            cities = cities.OrderBy(t => (int)GetDefinition(t).Name).ToList();
            
            foreach (var storage in cities)
            {
                storage.Clear();
                GetDefinition(storage).Clear();

                foreach (var (key, value) in _ignoredTrades)
                    storage.AddProducable(key, value);
            }

            foreach (var (item, config) in _trades)
            {
                var storage = FindStorage(config.City, cities);
                var definition = GetDefinition(storage);
                definition.AddMost(item);
            }

            foreach (var storage in cities)
            {
                var city = GetCity(storage);
                
                foreach (var (item, config) in _trades)
                {
                    if (city != config.City)
                        continue;
                    
                    var others = new List<CityStorage>();
                    others.AddRange(cities);
                    others.Remove(storage);
                    GenerateTrade(item, config, storage, others);
                }
            }
            
            // foreach (var (item, config) in _trades)
            // {
            //     var city = FindStorage(config.City, cities);
            //     var others = new List<CityStorage>();
            //     others.AddRange(cities);
            //     others.Remove(city);
            //     GenerateTrade(item, config, city, others);
            // }

#if UNITY_EDITOR
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
#endif
        }

        private void GenerateTrade(
            ItemType itemType,
            GeneratableTradeConfig config,
            CityStorage storage,
            List<CityStorage> othersSource)
        {
            var others = new List<CityStorage>();
            others.AddRange(othersSource);

            others = others
                .OrderByDescending(t => CompareDistance(storage, t)).ToList();

            var result = "";
            foreach (var other in others)
            {
                result += $"{GetDefinition(other).Name} ";
            }
            
            Debug.Log($"Origin: {result}");
            
            var count = others.Count;

            var mainConfig = new ItemPriceConfig(
                config.MedianCost,
                config.MedianCurveHeight,
                config.MedianCount,
                config.MaxItems,
                config.ProductionSpeed, itemType);
            
#if UNITY_EDITOR
            Undo.RecordObject(storage, "Assign tradable");
#endif
            storage.AddProducable(itemType, mainConfig);
#if UNITY_EDITOR
            Undo.RecordObject(storage, "Assign tradable");
            EditorUtility.SetDirty(storage);
#endif

            var mostCounter = 0;
            
            var reversed = others
                .OrderBy(t => CompareDistance(storage, t)).ToList();
            
            result = "";
            foreach (var other in reversed)
            {
                result += $"{GetDefinition(other).Name} ";
            }
            
            Debug.Log($"Reversed: {result}");

            foreach (var other in reversed)
            {
                var definition = GetDefinition(other);

                if (definition.MostProduced.Count == 3)
                    continue;

#if UNITY_EDITOR
                Undo.RecordObject(definition, "Assigned");
#endif
                definition.AddMost(itemType);
                mostCounter++;
                
#if UNITY_EDITOR
                Undo.RecordObject(definition, "Assigned");
                EditorUtility.SetDirty(definition);
#endif
                
                if (mostCounter == 2)
                    break;
            }
            
            var leastCounter = 0;

            foreach (var other in others)
            {
                var definition = GetDefinition(other);

                if (definition.LeastProduced.Count == 3 || definition.MostProduced.Contains(itemType) == true)
                    continue;

#if UNITY_EDITOR
                Undo.RecordObject(definition, "Assigned");
#endif
                definition.AddLeast(itemType);
                leastCounter++;
                
#if UNITY_EDITOR
                Undo.RecordObject(definition, "Assigned");
                EditorUtility.SetDirty(definition);
#endif
                
                if (leastCounter == 3)
                    break;
            }

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

#if UNITY_EDITOR
                Undo.RecordObject(others[i], "Assign tradable");
#endif
                others[i].AddProducable(itemType, price);

#if UNITY_EDITOR
                EditorUtility.SetDirty(others[i]);
                Undo.RecordObject(others[i], "Assign tradable");
#endif
            }
        }

        private CityStorage FindStorage(CityType target, List<CityStorage> all)
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

        private float CompareDistance(CityStorage a, CityStorage b)
        {
            var aPosition = a.transform.position;
            var bPosition = b.transform.position;
            var distance = Vector2.Distance(aPosition, bPosition);
            Debug.Log($"{GetDefinition(a).Name} to {GetDefinition(b).Name} = {distance}");
            return distance;
        }
    }
}