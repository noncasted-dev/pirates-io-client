using GamePlay.Services.Projectiles.Entity;
using UnityEngine.AddressableAssets;

namespace Features.GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    public interface IShooterConfig
    {
        AssetReference Projectile { get; }
        AssetReference Vfx { get; }
        int ShotsAmount { get; }
        float ShotsDelay { get; }
        ShootParams CreateParams();
    }
}