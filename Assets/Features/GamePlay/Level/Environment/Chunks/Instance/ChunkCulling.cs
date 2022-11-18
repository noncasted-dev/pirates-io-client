using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Instance
{
    [DisallowMultipleComponent]
    public class ChunkCulling : MonoBehaviour, IChunkCulling
    {
        [SerializeField] private GameObject[] _cullingObjects;

        public void Enable()
        {
            foreach (var cullingObject in _cullingObjects)
                cullingObject.SetActive(true);
        }

        public void Disable()
        {
            foreach (var cullingObject in _cullingObjects)
                cullingObject.SetActive(false);
        }
    }
}