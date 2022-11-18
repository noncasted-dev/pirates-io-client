using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Instance
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private ChunkCulling _chunkCulling;
        
        public Vector2 position => transform.position;
        public IChunkCulling Culling => _chunkCulling;
    }
}