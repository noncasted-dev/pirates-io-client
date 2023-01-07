using GamePlay.Level.Environment.Chunks.Instance;
using UnityEditor;
using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Editor
{
    public static class ChunkIndexer
    {
        [MenuItem("Tools/IndexChunks")]
        public static void IndexChunks()
        {
            var chunks = Object.FindObjectsOfType<Chunk>();

            foreach (var chunk in chunks)
            {
                var index = chunk.name.Replace("Chunk_", "");
                var numbers = index.Split("_");

                var x = int.Parse(numbers[0]);
                var y = int.Parse(numbers[1]);

                Undo.RecordObject(chunk, "Construct chunk");
                chunk.OnRename(x, y);
                Undo.RecordObject(chunk, "Construct chunk.");
            }
        }
    }
}