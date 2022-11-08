using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Dash;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "RangeAttack",
        menuName = PlayerAssetsPaths.RangeAttack + "State")]
    public class RangeAttackAsset : PlayerComponentAsset
    {
        [SerializeField] [EditableObject] private RangeAttackAnimationTriggerAsset _animation;
        [SerializeField] [EditableObject] private RangeAttackConfigAsset _config;
        [SerializeField] [EditableObject] private RangeAttackLogSettings _logSettings;
        [SerializeField] [EditableObject] private RangeAttackDefinition _definition;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<RangeAttackLogger>(Lifetime.Scoped)
                .WithParameter("settings", _logSettings);

            builder.Register<RangeAttackAnimatorCallbackBridge>(Lifetime.Singleton).AsSelf();

            var animation = _animation.CreateTrigger();

            builder.Register<RangeAttack>(Lifetime.Scoped)
                .WithParameter("definition", _definition)
                .WithParameter("animation", animation)
                .As<IRangeAttack>()
                .AsSelf();

            builder.Register<RangeAttackDash>(Lifetime.Scoped).As<IRangeAttackDash>();
            builder.Register<DashDirection>(Lifetime.Scoped).As<IDashDirection>();
            builder.Register<RangeAttackInput>(Lifetime.Scoped).AsSelf();
            builder.Register<RangeAttackRotator>(Lifetime.Scoped).As<IRangeAttackRotator>();

            builder.Register<RangeAttackConfig>(Lifetime.Scoped)
                .WithParameter("asset", _config)
                .As<IRangeAttackConfig>();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<RangeAttackInput>());
            callbackRegister.Add(resolver.Resolve<RangeAttack>());
        }
    }
}