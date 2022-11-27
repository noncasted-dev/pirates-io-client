using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GamePlay.Level.Environment.Tools;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Editor
{
    public static class ChunkRegenerator
    {
        [MenuItem("Environment/RegenerateGround")]
        public static void RegenerateGround()
        {
            Process().Forget();
        }

        private static async UniTaskVoid Process()
        {
            var first = Object.FindObjectsOfType<FirstLayerBuilder>();
            var second = Object.FindObjectsOfType<SecondLayerBuilder>();

            var firstList = new List<UniTask>();
            var secondList = new List<UniTask>();

            foreach (var builder in first)
                firstList.Add(builder.ProcessGenerate());

            foreach (var builder in second)
                secondList.Add(builder.ProcessGenerate());

            var all = new List<UniTask>();
            all.AddRange(firstList);
            all.AddRange(secondList);

            await UniTask.WhenAll(all);

            foreach (var chunk in first)
            {
                var gameObject = chunk.gameObject;
                gameObject.SetActive(false);
                EditorSceneManager.SaveScene(gameObject.scene);
                gameObject.SetActive(true);
                EditorSceneManager.SaveScene(gameObject.scene);
            }

            foreach (var chunk in second)
            {
                var gameObject = chunk.gameObject;
                gameObject.SetActive(false);
                EditorSceneManager.SaveScene(gameObject.scene);
                gameObject.SetActive(true);
                EditorSceneManager.SaveScene(gameObject.scene);
            }
        }
    }
}