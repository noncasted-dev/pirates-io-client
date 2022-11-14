using UnityEngine;

namespace GamePlay.Services.Projectiles.Implementation.Linear.Runtime
{
    public class MovementData
    {
        public MovementData(Vector2 direction, float speed, float distance)
        {
            Direction = direction;
            Speed = speed;
            Distance = distance;
        }

        public readonly Vector2 Direction;
        public readonly float Distance;
        public readonly float Speed;
    }
}