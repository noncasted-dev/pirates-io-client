using GamePlay.Services.PlayerPositionProviders.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
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

        [SerializeField] private Vector2 _rescale = new(0.1f, 0.1f);
        [SerializeField] private Vector2 _offset = new(0.1f, 0.1f);

        [SerializeField] private RectTransform _playerView;

        private IPlayerEntityProvider _player;
        private IUpdater _updater;

        public void OnOpened()
        {
            _updater.Add(this);
        }

        public void OnClosed()
        {
            _updater.Remove(this);
        }

        public void OnUpdate(float delta)
        {
            var worldPosition = _player.Position;
            var mapPosition = worldPosition / _rescale;
            _playerView.anchoredPosition = _offset + mapPosition;
        }
    }
}