namespace GamePlay.Services.Projectiles.Mover.Abstract
{
    public interface IProjectilesMover
    {
        void Add(IMovableProjectile projectile);
        void Remove(IMovableProjectile projectile);
    }
}