using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using static GamePlay.Player.Entity.Setup.Path.PlayerAssetsPaths;

namespace GamePlay.Player.Entity.States.Deaths.Runtime
{
    [CreateAssetMenu(fileName = StatePrefix + "BotDeath",
        menuName = PlayerAssetsPaths.Death + "Bot")]
    public class BotDeathAsset : DeathAsset
    {
        [SerializeField] private DeathStateDefinition _def;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<BotDeath>()
                .WithParameter(_def)
                .As<IDeath>()
                .AsCallbackListener();
        }
    }
}