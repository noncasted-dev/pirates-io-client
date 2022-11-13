using UnityEngine;

namespace GamePlay.Services.Projectiles.Entity
{
    public interface IProjectileStarter
    {
        void Fire(
            Vector2 direction,
            ShootParams shootParams,
            bool isLocal);
    }
}