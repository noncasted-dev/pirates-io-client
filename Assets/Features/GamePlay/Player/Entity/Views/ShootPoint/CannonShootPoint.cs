using Common.Structs;
using UnityEngine;

namespace GamePlay.Player.Entity.Views.ShootPoint
{
    [DisallowMultipleComponent]
    public class CannonShootPoint : MonoBehaviour, IShootPoint
    {
        private const float _distance = 2f;

        [SerializeField] private Transform _center;
        [SerializeField] [Min(0f)] private float _offset;
        [SerializeField] private LayerMask _shootAreaMask;

        private readonly RaycastHit2D[] _buffer = new RaycastHit2D[1];

        public Vector2 GetShootPoint(float angle)
        {
            var origin = (Vector2)_center.position;
            var direction = AngleUtils.ToDirection(angle);

            var hit = Physics2D.RaycastNonAlloc(
                origin,
                direction,
                _buffer,
                _distance,
                _shootAreaMask);

            if (hit == 0)
                return transform.position;

            var point = _buffer[0].point;
            point += direction * _offset;

            return point;
        }
    }
}