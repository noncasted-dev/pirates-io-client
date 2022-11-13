using GamePlay.Services.Projectiles.Entity;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    public class ShooterConfig : IShooterConfig
    {
        public ShooterConfig(ShooterConfigAsset asset)
        {
            _asset = asset;
        }

        private readonly ShooterConfigAsset _asset;

        public AssetReference Projectile => _asset.Projectile;
        public AssetReference Vfx => _asset.Vfx;
        public int ShotsAmount => _asset.ShotsAmount;
        public float ShotsDelay => _asset.ShotsDelay;
        public float ShotRandomDelay => _asset.ShotRandomDelay;

        public ShootParams CreateParams()
        {
            return new ShootParams(
                _asset.Damage,
                _asset.PushForce,
                _asset.Speed, 
                _asset.Distance,
                _asset.LayerMask);
        }
    }
}