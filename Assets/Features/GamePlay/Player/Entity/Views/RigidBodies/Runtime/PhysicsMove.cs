#region

using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Views.RigidBodies.Runtime
{
    public readonly struct PhysicsMove
    {
        public PhysicsMove(Vector2 direction, float distance)
        {
            Direction = direction;
            Distance = distance;
        }

        public readonly Vector2 Direction;
        public readonly float Distance;
    }
}