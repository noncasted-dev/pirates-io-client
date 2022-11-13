using UnityEngine;

namespace GamePlay.Services.Projectiles.Mover
{
    public struct ProjectileMoveData
    {
        public ProjectileMoveData(
            Vector2 currentPosition,
            Vector2 direction,
            float speed)
        {
            _currentPosition = currentPosition;
            _previousPosition = currentPosition;
            Direction = direction;
            Speed = speed;
            _middlePoint = Vector2.zero;
            _passedDistance = 0f;
        }

        private const float _lerpMiddleValue = 0.5f;

        public readonly Vector2 Direction;
        public readonly float Speed;

        private readonly Vector2 _previousPosition;

        private Vector2 _currentPosition;
        private Vector2 _middlePoint;
        private float _passedDistance;

        public Vector2 CurrentPosition => _currentPosition;
        public Vector2 MiddlePoint => _middlePoint;
        public float PassedDistance => _passedDistance;

        public void SetPosition(Vector2 position)
        {
            _currentPosition = position;
            _middlePoint = Vector2.Lerp(_previousPosition, _currentPosition, _lerpMiddleValue);
            _passedDistance = Vector2.Distance(_previousPosition, _currentPosition);
        }
    }
}