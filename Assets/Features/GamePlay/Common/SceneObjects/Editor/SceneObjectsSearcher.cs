#region

using System.Collections.Generic;
using GamePlay.Common.SceneObjects.Runtime;
using UnityEditor;
using UnityEngine;

#endregion

namespace GamePlay.Common.SceneObjects.Editor
{
    public class SceneObjectsSearcher : AssetModificationProcessor
    {
        private static string[] OnWillSaveAssets(string[] paths)
        {
            Debug.Log("Scanning for SceneObjects");

            var behaviours = Object.FindObjectsOfType<MonoBehaviour>(true);

            var objects = new List<SceneObject>();

            foreach (var behaviour in behaviours)
                if (behaviour is SceneObject sceneObject)
                    objects.Add(sceneObject);

            var storage = FindSceneObjectsHandler(behaviours);

            if (storage == null)
                return paths;

            Undo.RecordObject((MonoBehaviour)storage, "Set objects");
            storage.SetObjects(objects.ToArray());

            return paths;
        }

        private static ISceneObjectsStorage FindSceneObjectsHandler(IEnumerable<MonoBehaviour> behaviours)
        {
            foreach (var behaviour in behaviours)
                if (behaviour is ISceneObjectsStorage filler)
                    return filler;

            Debug.LogWarning("No SceneObjectsFiller in scene found");

            return null;
        }
    }
}