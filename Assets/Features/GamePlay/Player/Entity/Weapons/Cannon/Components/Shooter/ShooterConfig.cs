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
        public int ShotsAmount => _asset.ShotsAmount;

        public AssetReference Ball => _asset.Ball;
        public AssetReference Knuppel => _asset.Knuppel;
        public AssetReference Shrapnel => _asset.Shrapnel;
        public AssetReference Fishnet => _asset.Fishnet;
        public AssetReference Vfx => _asset.Vfx;
        public float ShotsDelay => _asset.ShotsDelay;
        public float ShotRandomDelay => _asset.ShotRandomDelay;
        public float RandomDistance => _asset.RandomDistance;

        public ShootParams CreateParams(float additionalDistance)
        {
            return new ShootParams(
                _asset.Damage,
                _asset.Speed,
                _asset.Distance + additionalDistance);
        }
    }
}