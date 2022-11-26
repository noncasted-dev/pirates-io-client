using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.BowComponentPrefix + "BowShooter",
        menuName = PlayerAssetsPaths.BowShooter + "Component")]
    public class ShooterAsset : PlayerComponentAsset
    {
        [SerializeField]  private ShooterConfigAsset _config;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<ShooterConfig>(Lifetime.Scoped)
                .WithParameter(_config)
                .As<IShooterConfig>();

            builder.Register<Shooter>(Lifetime.Scoped)
                .As<IShooter>()
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<Shooter>());
        }
    }
}