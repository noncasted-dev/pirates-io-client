using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.BowComponentPrefix + "BowShooter",
        menuName = PlayerAssetsPaths.BowShooter + "Component")]
    public class ShooterAsset : PlayerComponentAsset
    {
        [SerializeField] private ShooterConfigAsset _config;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<ShooterConfig>()
                .WithParameter(_config)
                .As<IShooterConfig>();

            builder.Register<Shooter>()
                .As<IShooter>()
                .AsCallbackListener();
        }
    }
}