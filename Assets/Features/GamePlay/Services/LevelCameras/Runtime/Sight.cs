using UnityEngine;

namespace GamePlay.Services.LevelCameras.Runtime
{
    public readonly struct Sight
    {
        public Sight(
            Vector2 direction,
            float distance,
            bool isOversight)
        {
            _direction = direction;
            _distance = distance;
            IsOversight = isOversight;
        }

        public readonly bool IsOversight;

        private readonly Vector3 _direction;
        private readonly float _distance;

        public Vector3 CreateOversightMove()
        {
            return _direction * _distance;
        }
    }
}