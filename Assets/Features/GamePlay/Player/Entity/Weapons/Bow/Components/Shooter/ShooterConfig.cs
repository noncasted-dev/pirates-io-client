using GamePlay.Services.Projectiles.Entity;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Weapons.Bow.Components.Shooter
{
    public class ShooterConfig : IShooterConfig
    {
        public ShooterConfig(ShooterConfigAsset asset)
        {
            _asset = asset;
        }

        private readonly ShooterConfigAsset _asset;

        public AssetReference Reference => _asset.Reference;

        public ShootParams CreateParams()
        {
            return new ShootParams(_asset.Damage, _asset.PushForce, _asset.Speed, _asset.LayerMask);
        }
    }
}