using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.Healths.Logs;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "Health",
        menuName = PlayerAssetsPaths.Health + "Component")]
    public class HealthAsset : PlayerComponentAsset
    {
        [SerializeField] private HealthLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<HealthLogger>()
                .WithParameter(_logSettings);

            builder.Register<Health>()
                .As<IHealth>()
                .AsCallbackListener();

            builder.Register<Sail>()
                .As<ISail>();
        }
    }
}