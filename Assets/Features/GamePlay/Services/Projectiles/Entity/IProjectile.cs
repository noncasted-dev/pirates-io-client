using GamePlay.Common.Damages;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Entity
{
    public interface IProjectile
    {
        LayerMask LayerMask { get; }
        Vector2 Position { get; }
        Vector2 Direction { get; }
        float ColliderHeight { get; }
        float Angle { get; }
        float Speed { get; }

        void Fire(
            Vector2 position,
            Vector2 direction,
            ShootParams shootParams);

        void SetPosition(Vector2 position);
        void OnCollided();
        void OnTriggered(IDamageReceiver damageReceiver);
    }
}