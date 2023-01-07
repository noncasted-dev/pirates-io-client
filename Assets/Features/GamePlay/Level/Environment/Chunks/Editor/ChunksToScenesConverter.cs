using GamePlay.Level.Environment.Chunks.Instance;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.Level.Environment.Chunks.Editor
{
    public static class ChunksToScenesConverter
    {
        private const string _path = "Assets/Features/GamePlay/Level/Scenes/Chunks/";

        [MenuItem("Tools/ConvertChunksToScenes")]
        public static void ConvertChunksToScenes()
        {
            var chunks = Object.FindObjectsOfType<Chunk>();

            foreach (var chunk in chunks)
                CreateScene(chunk);
        }

        private static void CreateScene(Chunk chunk)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
            chunk.gameObject.transform.parent = null;
            SceneManager.MoveGameObjectToScene(chunk.gameObject, scene);
            var name = $"Chunk_{chunk.X}_{chunk.Y}";
            EditorSceneManager.SaveScene(scene, _path + name + ".unity");
        }
    }
}