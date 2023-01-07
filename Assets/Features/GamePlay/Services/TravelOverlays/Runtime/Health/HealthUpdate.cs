using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Services.TravelOverlays.Runtime.Health
{
    public class HealthUpdate : IUpdatable
    {
        public HealthUpdate(
            IUpdater updater,
            RectTransform actual,
            RectTransform damage,
            float length,
            float speed)
        {
            _updater = updater;
            _actual = actual;
            _damage = damage;
            _length = length;
            _speed = speed;
            _ySize = _actual.sizeDelta.y;
        }

        private readonly RectTransform _actual;
        private readonly RectTransform _damage;

        private readonly float _length;
        private readonly float _speed;

        private readonly IUpdater _updater;
        private readonly float _ySize;
        private bool _isStarted;

        private float _targetLength;

        public void OnUpdate(float delta)
        {
            var length = Mathf.MoveTowards(_damage.sizeDelta.x, _targetLength, _speed * delta);
            _damage.sizeDelta = new Vector2(length, _ySize);
        }

        public void Start()
        {
            if (_isStarted == true)
                return;

            _isStarted = true;

            _updater.Add(this);
        }

        public void Stop()
        {
            _isStarted = false;

            _updater.Remove(this);
        }

        public void OnHealthChanged(int health, int maxHealth)
        {
            var healthLength = health / (float)maxHealth * _length;
            _actual.sizeDelta = new Vector2(healthLength, _ySize);
            _targetLength = healthLength;
        }
    }
}