using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Tools
{
    public class ChunkRename : MonoBehaviour
    {
        [SerializeField] private int _inRow = 20;
        [SerializeField] private int _chunkSize = 64;
        [SerializeField] private Vector2 _start = new(-640, 640);

        [Button("Rename")]
        private void Rename()
        {
            var childs = GetComponentsInChildren<ChunkBorderDrawer>()
                .Select(t => t.gameObject).ToArray();

            var x = 0;
            var y = 0;

            foreach (var chunk in childs)
            {
                chunk.name = $"Chunk_{x}_{y}";

                chunk.transform.position = _start + new Vector2(x * 64, -y * 64);

                x++;

                if (x == _inRow)
                {
                    x = 0;
                    y++;
                }
            }
        }
    }
}