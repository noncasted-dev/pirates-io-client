using GamePlay.Common.Damages;

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