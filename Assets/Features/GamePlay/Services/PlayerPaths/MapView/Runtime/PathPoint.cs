using UnityEngine;

namespace Features.GamePlay.Services.PlayerPaths.MapView.Runtime
{
    public class PathPoint
    {
        public PathPoint(GameObject gameObject)
        {
            _gameObject = gameObject;
            _transform = gameObject.GetComponent<RectTransform>();
        }
        
        private readonly GameObject _gameObject;
        private readonly RectTransform _transform;    

        public void Enable(Vector2 position, float rotation)
        {
            _gameObject.SetActive(true);

            _transform.anchoredPosition = position;
            _transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
        }

        public void Disable()
        {
            _gameObject.SetActive(false);
        }
    }
}