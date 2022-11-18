using GamePlay.Common.Paths;
using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Instance
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "Chunk",
        menuName = GamePlayAssetsPaths.ChunkConfig)]
    public class ChunkConfigAsset : ScriptableObject
    {
        [SerializeField] [Min(1)] private int _size;

        public int Size => _size;
    }
}