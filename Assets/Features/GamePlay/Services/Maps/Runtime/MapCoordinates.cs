using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Services.Maps.Runtime
{
    [DisallowMultipleComponent]
    public class MapCoordinates : MonoBehaviour
    {
        [SerializeField] private float _horizontalMultiplier = 0.5f;
        [SerializeField] private float _verticalMultiplier = 0.5f;
        
        [SerializeField] private ScrollRect _scroll;

        [SerializeField] private RectTransform _upScroll;
        [SerializeField] private RectTransform _downScroll;
        [SerializeField] private RectTransform _leftScroll;
        [SerializeField] private RectTransform _rightScroll;

        private Vector2 _upStart;
        private Vector2 _leftStart;
        private Vector2 _downStart;
        private Vector2 _rightStart;

        private void Awake()
        {
            _upStart = _upScroll.anchoredPosition;
            _leftStart = _leftScroll.anchoredPosition;
            _downStart = _downScroll.anchoredPosition;
            _rightStart = _rightScroll.anchoredPosition;
        }

        private void Update()
        {
            var vertical = _scroll.verticalNormalizedPosition;
            var horizontal = _scroll.horizontalNormalizedPosition;

            _upScroll.anchoredPosition =
                new Vector2(_upStart.x - _upScroll.sizeDelta.x * horizontal * _horizontalMultiplier, _upStart.y);
            
            _downScroll.anchoredPosition =
                new Vector2(_downStart.x - _downScroll.sizeDelta.x * horizontal * _horizontalMultiplier, _downStart.y);
            
            _leftScroll.anchoredPosition = new Vector2(_leftStart.x,
                _leftStart.y - _leftScroll.sizeDelta.x * vertical * _verticalMultiplier);
            
            _rightScroll.anchoredPosition = new Vector2(_rightStart.x,
                _rightStart.y - _rightScroll.sizeDelta.x * vertical * _verticalMultiplier);
        }
    }
}