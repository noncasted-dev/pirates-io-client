using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Level.Environment.Chunks.Instance
{
    [Serializable]
    public class ChunkSceneData
    {
        public ChunkSceneData(AssetReference reference, string path)
        {
            _reference = reference;
            _path = path;
        }
        
        [SerializeField] private AssetReference _reference;
        [SerializeField] private string _path;

        public AssetReference Reference => _reference;
        public string Path => _path;
    }
}