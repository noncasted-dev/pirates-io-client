using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "RangeAttack",
        menuName = PlayerAssetsPaths.RangeAttack + "State")]
    public class RangeAttackAsset : PlayerComponentAsset
    {
        [SerializeField] private RangeAttackConfigAsset _config;
        [SerializeField] private RangeAttackDefinition _definition;
        [SerializeField] private RangeAttackLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<RangeAttackLogger>()
                .WithParameter(_logSettings);

            builder.Register<RangeAttack>()
                .WithParameter(_definition)
                .As<IRangeAttack>()
                .AsCallbackListener();

            builder.Register<RangeAttackInput>()
                .AsCallbackListener();

            builder.Register<RangeAttackConfig>()
                .WithParameter(_config)
                .As<IRangeAttackConfig>();
        }
    }
}