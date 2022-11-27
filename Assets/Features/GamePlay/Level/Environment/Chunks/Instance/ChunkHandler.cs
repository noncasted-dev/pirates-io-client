using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace GamePlay.Level.Environment.Chunks.Instance
{       
    public class ChunkHandler : MonoBehaviour
    {
        [SerializeField] private AssetReference _scene;

        private bool _isLoaded;
        
        private SceneInstance _loaded;
        
        public AssetReference Scene => _scene;
        public SceneInstance Loaded => _loaded;
        public Vector2 Position => transform.position;
        public bool IsLoaded => _isLoaded;
        

        public void Construct(AssetReference scene)
        {
            _scene = scene;
        }

        public void MarkAsLoaded()
        {
            _isLoaded = true;
        }
        
        public void OnLoaded(SceneInstance scene)
        {
            _loaded = scene;
        }

        public void OnUnloaded()
        {
            _isLoaded = false;
        }
    }
}