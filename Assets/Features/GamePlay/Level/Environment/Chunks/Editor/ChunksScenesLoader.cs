using UnityEditor;
using UnityEditor.SceneManagement;

namespace GamePlay.Level.Environment.Chunks.Editor
{
    public static class ChunksScenesLoader
    {
        private const string _path = "Assets/Features/GamePlay/Level/Scenes/Chunks/";

        private const int _x = 28;
        private const int _y = 16;
        //piska2
        [MenuItem("Tools/LoadAllChunks")]
        public static void LoadAll()
        {
            for (var x = 0; x < _x; x++)
            {
                for (var y = 0; y < _y; y++)
                {
                    EditorSceneManager.OpenScene(_path + $"Chunk_{x}_{y}.unity", OpenSceneMode.Additive);
                }
            }
        }
    }
}