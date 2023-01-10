using UnityEngine;

namespace GamePlay.Services.Maps.Runtime
{
    [DisallowMultipleComponent]
    public class WorldToMapPositionConverter : MonoBehaviour
    {
        [SerializeField] private Transform _levelLeftBottom;
        [SerializeField] private Transform _levelLeftTop;
        [SerializeField] private Transform _levelRightBottom;

        [SerializeField] private RectTransform _mapLeftBottom;
        [SerializeField] private RectTransform _mapLeftTop;
        [SerializeField] private RectTransform _mapRightBottom;
        
        public Vector2 ConvertWorldToMap(Vector2 world)
        {
            var leftBottom = _levelLeftBottom.transform.position;
            
            var levelWidth = Vector2.Distance(
                leftBottom,
                _levelRightBottom.transform.position);

            var levelHeight = Vector2.Distance(
                leftBottom,
                _levelLeftTop.transform.position);

            var xProgress = (world.x + Mathf.Abs(leftBottom.x)) / levelWidth;
            var yProgress = (world.y + Mathf.Abs(leftBottom.y)) / levelHeight;

            var mapWidth = Vector2.Distance(
                _mapLeftBottom.anchoredPosition,
                _mapRightBottom.anchoredPosition);

            var mapHeight = Vector2.Distance(
                _mapLeftBottom.anchoredPosition,
                _mapLeftTop.anchoredPosition);

            var progress = new Vector2(xProgress, yProgress);
            var position = _mapLeftBottom.anchoredPosition + new Vector2(mapWidth, mapHeight) * progress;

            return position;
        }
    }
}