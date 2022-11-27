using GamePlay.Level.Environment.Tools;
using UnityEditor;
using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Editor
{
    public static class ChunkRegenerator 
    {
        [MenuItem("Environment/RegenerateGround")]
        public static void RegenerateGround()
        {
            var first = Object.FindObjectsOfType<FirstLayerBuilder>();
            var second = Object.FindObjectsOfType<SecondLayerBuilder>();

            foreach (var builder in first)
                builder.Generate();
            
            foreach (var builder in second)
                builder.Generate();
        }
    }
}