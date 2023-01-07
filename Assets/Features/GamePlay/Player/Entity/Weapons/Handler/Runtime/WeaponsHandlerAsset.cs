using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.Weapons.Handler.Logs;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Handler.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "WeaponsHandler",
        menuName = PlayerAssetsPaths.WeaponsHandler + "Component")]
    public class WeaponsHandlerAsset : PlayerComponentAsset
    {
        [SerializeField] private DefaultWeaponsConfig _config;
        [SerializeField] private WeaponsHandlerLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<WeaponsHandlerLogger>()
                .WithParameter(_logSettings);

            builder.Register<WeaponsFactory>()
                .As<IWeaponsFactory>();

            builder.Register<WeaponsHandler>()
                .WithParameter(_config)
                .As<IWeaponsHandler>()
                .AsCallbackListener();
        }
    }
}