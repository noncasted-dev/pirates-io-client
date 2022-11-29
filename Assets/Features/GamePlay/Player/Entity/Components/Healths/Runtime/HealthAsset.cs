using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.Healths.Logs;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "Health",
        menuName = PlayerAssetsPaths.Health + "Component")]
    public class HealthAsset : PlayerComponentAsset
    {
        [SerializeField]  private HealthLogSettings _logSettings;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<HealthLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);
            
            builder.Register<Health>(Lifetime.Scoped)
                .As<IHealth>()
                .AsSelf();
            
            builder.Register<Sail>(Lifetime.Scoped)
                .As<ISail>();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            var health = resolver.Resolve<Health>();
            callbackRegister.Add(health);
        }
    }
}