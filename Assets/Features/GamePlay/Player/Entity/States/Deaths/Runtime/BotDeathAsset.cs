using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;
using static GamePlay.Player.Entity.Setup.Path.PlayerAssetsPaths;

namespace GamePlay.Player.Entity.States.Deaths.Runtime
{
    [CreateAssetMenu(fileName = StatePrefix + "BotDeath",
        menuName = PlayerAssetsPaths.Death + "Bot")]
    public class BotDeathAsset : DeathAsset
    {
        [SerializeField] private DeathStateDefinition _def;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<BotDeath>(Lifetime.Scoped)
                .WithParameter(_def)
                .As<IDeath>()
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<BotDeath>());
        }
    }
}