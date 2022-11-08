using GamePlay.Services.Projectiles.Entity;

namespace GamePlay.Services.Projectiles.Mover
{
    public interface IProjectilesMover
    {
        void Add(IProjectile projectile);
        void Remove(IProjectile projectile);
    }
}