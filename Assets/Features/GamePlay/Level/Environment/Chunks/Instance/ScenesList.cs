using System.Collections.Generic;
using GamePlay.Common.Paths;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Level.Environment.Chunks.Instance
{
    [CreateAssetMenu(fileName = "ScenesList", menuName = GamePlayAssetsPaths.Config + "ScenesList")]
    public class ScenesList : ScriptableObject
    {
        [SerializeField] private List<ChunkSceneData> _scenes;

        public IEnumerable<ChunkSceneData> Scenes => _scenes;

        public void Clear()
        {
            _scenes = new List<ChunkSceneData>();
        }

        public void Add(AssetReference scene, string path)
        {
            _scenes.Add(new ChunkSceneData(scene, path));
        }
    }
}