using UnityEngine;

namespace GamePlay.Player.Entity.Components.InertialMovements.Runtime
{
    public interface IInertialMovement
    {
        float XDirection { get; }
        void Enable();
        void Disable();
        void SetDirection(Vector2 direction);
        void ResetDirection();
        void SetSpeed(float speed);
    }
}