using GamePlay.Services.PlayerPositionProviders.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Features.GamePlay.Services.Maps.Runtime
{
    public class MapPlayerMover : MonoBehaviour, IUpdatable
    {
        [Inject]
        private void Construct(
            IPlayerEntityProvider player,
            IUpdater updater)
        {
            _updater = updater;
            _player = player;
        }

        [SerializeField] private Transform _levelLeftBottom;
        [SerializeField] private Transform _levelLeftTop;
        [SerializeField] private Transform _levelRightBottom;

        [SerializeField] private RectTransform _mapLeftBottom;
        [SerializeField] private RectTransform _mapLeftTop;
        [SerializeField] private RectTransform _mapRightBottom;

        [SerializeField] private RectTransform _playerView;
        [SerializeField] private RectTransform _content;
        [SerializeField] private ScrollRect _scroll;

        private IPlayerEntityProvider _player;
        private IUpdater _updater;

        public void OnOpened()
        {
            _updater.Add(this);
            
            Canvas.ForceUpdateCanvases();

            _content.anchoredPosition =
                (Vector2)_scroll.transform.InverseTransformPoint(_content.position)
                - (Vector2)_scroll.transform.InverseTransformPoint(_playerView.position);
        }

        public void OnClosed()
        {
            _updater.Remove(this);
        }

        public void OnUpdate(float timeDelta)
        {
            var levelWidth = Vector2.Distance(
                    _levelLeftBottom.transform.position,
                    _levelRightBottom.transform.position);
            
            var levelHeight = Vector2.Distance(
                _levelLeftBottom.transform.position,
                _levelLeftTop.transform.position);

            var levelPlayer = _player.Position;
            levelPlayer += new Vector2(
                Mathf.Abs(_levelLeftBottom.transform.position.x),
                Mathf.Abs(_levelLeftBottom.transform.position.y));

            var delta = levelPlayer / new Vector2(levelWidth, levelHeight);
            
            var mapWidth = Vector2.Distance(
                _mapLeftBottom.anchoredPosition,
                _mapRightBottom.anchoredPosition);
            
            var mapHeight = Vector2.Distance(
                _mapLeftBottom.anchoredPosition,
                _mapLeftTop.anchoredPosition);

            var position = _mapLeftBottom.anchoredPosition + new Vector2(mapWidth, mapHeight) * delta;
            
            _playerView.anchoredPosition = position;
        }
    }
}