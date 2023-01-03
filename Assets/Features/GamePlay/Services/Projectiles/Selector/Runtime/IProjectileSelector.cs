using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;

namespace GamePlay.Services.Projectiles.Selector.Runtime
{
    public interface IProjectileSelector
    {
        ProjectileType Selected { get; }
        
        int GetAmount(ProjectileType type);
        bool CanSelect(ProjectileType type);
        void Select(ProjectileType type);
        bool CanShoot();
    }
}