#region

using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Views.RigidBodies.Runtime
{
    public interface IRigidBody
    {
        Vector2 Position { get; }

        void SetPosition(Vector2 position);
        void Move(Vector2 direction, float distance);
    }
}