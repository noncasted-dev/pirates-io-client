using Common.EditableScriptableObjects.Attributes;
using Features.GamePlay.Level.Environment.Chunks.Runtime;
using UnityEngine;

namespace Features.GamePlay.Level.Environment.Chunks.Editor
{
    [DisallowMultipleComponent]
    public class ChunkBorderDrawer : MonoBehaviour
    {
        [SerializeField] [EditableObject] private ChunkConfigAsset _config;
        
        private void OnDrawGizmos()
        {
            var position = transform.position;
            var size = new Vector3(_config.Size, _config.Size, 0f);
            
            Gizmos.DrawWireCube(position, size);
        }
    }
}