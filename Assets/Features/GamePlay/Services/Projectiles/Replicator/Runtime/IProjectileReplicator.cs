using GamePlay.Services.Projectiles.Entity;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Replicator.Runtime
{
    public interface IProjectileReplicator
    {
        void Replicate(
            ProjectileType type,
            Vector2 position,
            float angle,
            float speed,
            int damage,
            float distance);
    }
}