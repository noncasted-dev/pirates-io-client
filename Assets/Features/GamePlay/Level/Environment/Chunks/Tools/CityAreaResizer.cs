using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Level.Environment.Chunks.Tools
{
    public class CityAreaResizer : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private Transform _circle;

        [Button("Resize all")]
        private void ResizeAll()
        {
            var resizers = FindObjectsOfType<CityAreaResizer>();

            foreach (var resizer in resizers)
                resizer.Resize();
        }
        
        private void Resize()
        {
            var radius = _collider.radius;
            var scale = new Vector3(1f, 1f, 1f) / 9f;
            _circle.localScale = scale * radius;
        }
    }
}