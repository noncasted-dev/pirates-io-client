using Cysharp.Threading.Tasks;
using GamePlay.Level.Environment.Chunks.Instance;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace GamePlay.Level.Environment.Chunks.Editor
{
    public static class ChunksScanner
    {
        private const string _prefabPath = "Assets/Features/GamePlay/Level/Environment/Chunks/Config/";
        private const string _scenesPath = "Assets/Features/GamePlay/Level/Scenes/Chunks/";

        private const int _x = 16;
        private const int _y = 16;

        [MenuItem("Tools/ScanChunks")]
        public static void ScanChunks()
        {
            var prefab = Resources.Load<ChunkHandler>(_prefabPath + "ChunkHandler");
            var scenesList = Resources.Load<ScenesList>(_prefabPath + "ScenesList");

             foreach (var scene in scenesList.Scenes)
                 CreateHandler(prefab, scene).Forget();
        }

        [MenuItem("Tools/ScanScenes")]
        public static void ScanScenes()
        {
            var path = _prefabPath + "ScenesList";
            var scenesList = Resources.Load<ScenesList>("ScenesList");
            Debug.Log($"Load list at: {path} == {scenesList == null}");
            var settings = AddressableAssetSettingsDefaultObject.Settings;

            if (settings == null)
            {
                Debug.LogError("Settings is null");
                return;
            }

            var group = settings.FindGroup("Scenes");

            foreach (var entry in group.entries)
            {
                var reference = new AssetReference(entry.guid);

                Undo.RecordObject(scenesList, "List fill");
                scenesList.Add(reference);
                Undo.RecordObject(scenesList, "List fill");
            }
        }

        private static async UniTaskVoid ProcessSceneScanning(ScenesList list, int x, int y)
        {
        }

        private static async UniTaskVoid CreateHandler(ChunkHandler prefab, AssetReference scene)
        {
            var handle = Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive);

            var instance = await handle.Task;

            Chunk chunk = null;

            foreach (var rootObject in instance.Scene.GetRootGameObjects())
            {
                if (rootObject.TryGetComponent(out chunk) == true)
                    break;
            }

            var chunkHandler = Object.Instantiate(prefab, chunk.transform.position, Quaternion.identity);
            chunkHandler.name = $"ChunkHandler_{chunk.Y}_{chunk.Y}";
            Undo.RecordObject(chunkHandler, "Scene assign");
            chunkHandler.Construct(scene);
            Undo.RecordObject(chunkHandler, "Scene assign");
        }
    }
}