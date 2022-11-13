using UnityEngine;

namespace GamePlay.Services.Projectiles.Mover.Abstract
{
    public interface IProjectileMovement
    {
        ProjectileRaycastData RaycastData { get; }
        ProjectileMoveData CreateMoveData(float delta);

        void SetPosition(Vector2 position);
        void OnDistancePassed(float distance);
    }
}