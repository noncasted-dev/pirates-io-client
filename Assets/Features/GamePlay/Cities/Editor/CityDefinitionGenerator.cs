using System;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Factions.Common;
using UnityEditor;
using UnityEngine;

namespace GamePlay.Cities.Editor
{
    public static class CityDefinitionGenerator
    {
        private const string _path = "Assets/Features/GamePlay/Cities/Instance/Definitions/";

        [MenuItem("Tools/GenerateCitiesDefinitions")]
        public static void GenerateCitiesDefinitions()
        {
            foreach (var faction in (FactionType[])Enum.GetValues(typeof(FactionType)))
            foreach (var city in (CityType[])Enum.GetValues(typeof(CityType)))
            {
                var factionName = Enum.GetName(typeof(FactionType), faction);
                var rawCityName = Enum.GetName(typeof(CityType), city);

                if (rawCityName.Contains(factionName) == false)
                    continue;

                var definition = ScriptableObject.CreateInstance<CityDefinition>();
                definition.Construct(faction, city);

                var cityName = city.AsString();
                var name = $"City_{factionName}_{cityName}.asset";

                AssetDatabase.CreateAsset(definition, _path + name);
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = definition;
            }
        }
    }
}