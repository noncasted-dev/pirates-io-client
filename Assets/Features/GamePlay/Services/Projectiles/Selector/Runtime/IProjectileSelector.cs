namespace GamePlay.Services.Projectiles.Selector.Runtime
{
    public interface IProjectileSelector
    {
        ProjectileType Selected { get; }
        
        int GetAmount(ProjectileType type);
        bool CanShoot();
    }
}