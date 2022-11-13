using GamePlay.Common.Damages;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Entity
{
    public interface IProjectileStarter
    {
        void Fire(
            Vector2 position,
            Vector2 direction,
            ShootParams shootParams);
    }
}