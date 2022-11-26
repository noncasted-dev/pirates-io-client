using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Level.Environment.Chunks.Instance
{       
    public class ChunkHandler : MonoBehaviour
    {
        [SerializeField] private AssetReference _scene;

        public AssetReference Scene => _scene;

        public void Construct(AssetReference scene)
        {
            _scene = scene;
        }
    }
}