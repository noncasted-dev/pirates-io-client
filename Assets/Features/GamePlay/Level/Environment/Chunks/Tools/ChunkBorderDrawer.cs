using GamePlay.Level.Environment.Chunks.Instance;
using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Tools
{
    [DisallowMultipleComponent]
    public class ChunkBorderDrawer : MonoBehaviour
    {
        [SerializeField] private ChunkConfigAsset _config;

        private void OnDrawGizmos()
        {
            var position = transform.position;
            var size = new Vector3(_config.Size, _config.Size, 0f);

            Gizmos.DrawWireCube(position, size);
        }
    }
}