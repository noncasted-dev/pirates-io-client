using GamePlay.Services.Projectiles.Entity;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Weapons.Bow.Components.Shooter
{
    public interface IShooterConfig
    {
        AssetReference Reference { get; }
        ShootParams CreateParams();
    }
}