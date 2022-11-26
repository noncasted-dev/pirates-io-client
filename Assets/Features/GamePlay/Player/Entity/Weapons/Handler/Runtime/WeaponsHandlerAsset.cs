using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.Weapons.Handler.Logs;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Weapons.Handler.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "WeaponsHandler",
        menuName = PlayerAssetsPaths.WeaponsHandler + "Component")]
    public class WeaponsHandlerAsset : PlayerComponentAsset
    {
        [SerializeField]  private DefaultWeaponsConfig _config;
        [SerializeField]  private WeaponsHandlerLogSettings _logSettings;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<WeaponsHandlerLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            builder.Register<WeaponsFactory>(Lifetime.Scoped)
                .As<IWeaponsFactory>();
            builder.Register<WeaponsHandler>(Lifetime.Scoped)
                .WithParameter(_config)
                .As<IWeaponsHandler>()
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<WeaponsHandler>());
        }
    }
}