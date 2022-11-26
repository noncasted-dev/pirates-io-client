using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Instance
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private ChunkCulling _chunkCulling;
        [SerializeField] private int _x;
        [SerializeField] private int _y;
        public Vector2 position => transform.position;
        public IChunkCulling Culling => _chunkCulling;

        public int X => _x;
        public int Y => _y;

        public void OnRename(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}