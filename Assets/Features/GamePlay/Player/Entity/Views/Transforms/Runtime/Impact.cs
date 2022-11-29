using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.Views.Transforms.Runtime
{
    public class Impact : IFixedUpdatable
    {
        public Impact(
            IUpdater updater,
            ITransform transform,
            Vector2 direction,
            float distance,
            float time)
        {
            _updater = updater;
            _transform = transform;
            _direction = direction;
            _distance = distance;
            _time = time;
        }

        private readonly Vector2 _direction;
        private readonly float _distance;
        private readonly float _time;
        private readonly ITransform _transform;

        private readonly IUpdater _updater;

        private float _currentTime;

        private bool _isEnded;

        public bool IsEnded => _isEnded;

        public void OnFixedUpdate(float delta)
        {
            if (_isEnded == true)
                return;
            
            _currentTime += delta;

            var progress = _currentTime / _time;

            var direction = _direction;

            if (progress > 0.5f)
                direction *= -1;

            var position = _transform.Position;
            position += direction * (_distance * delta * 2f);
            _transform.SetPosition(position);

            if (progress < 1)
                return;

            _transform.SetLocalPosition(Vector2.zero);
            _updater.Remove(this);

            _isEnded = true;
        }

        public void Start()
        {
            _updater.Add(this);
        }

        public void Stop()
        {
            if (_isEnded == true)
                return;
            
            _updater.Remove(this);
        }
    }
}