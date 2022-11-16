using GamePlay.Services.Projectiles.Entity;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    public interface IShooterConfig
    {
        AssetReference Projectile { get; }
        AssetReference Vfx { get; }
        int ShotsAmount { get; }
        float ShotsDelay { get; }
        float ShotRandomDelay { get; }
        float RandomDistance { get; }
        ShootParams CreateParams(float additionalDistance);
    }
}