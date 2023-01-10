using GamePlay.Services.PlayerPositionProviders.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GamePlay.Services.Maps.Runtime
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

        [SerializeField] private RectTransform _playerView;
        [SerializeField] private RectTransform _content;
        [SerializeField] private ScrollRect _scroll;

        [SerializeField] private WorldToMapPositionConverter _converter;

        private IPlayerEntityProvider _player;
        private IUpdater _updater;

        public void OnUpdate(float timeDelta)
        {
            _playerView.anchoredPosition = _converter.ConvertWorldToMap(_player.Position);
        }

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
    }
}