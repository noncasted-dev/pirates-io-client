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
        [SerializeField] [EditableObject] private WeaponsHandlerLogSettings _logSettings;
        [SerializeField] [EditableObject] private DefaultWeaponsConfig _config;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<WeaponsHandlerLogger>(Lifetime.Scoped)
                .WithParameter("settings", _logSettings);

            builder.Register<WeaponsFactory>(Lifetime.Scoped)
                .As<IWeaponsFactory>();
            builder.Register<WeaponsHandler>(Lifetime.Scoped)
                .WithParameter("config", _config)
                .As<IWeaponsHandler>()
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<WeaponsHandler>());
        }
    }
}