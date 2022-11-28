using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.Views.ShipConfig.Runtime;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Dead;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.Sounds.Runtime;
using UniRx;

namespace GamePlay.Player.Entity.States.Deaths.Runtime
{
    public class Death : IDeath, IState, IAwakeCallback
    {
        public Death(
            IStateMachine stateMachine,
            IVfxPoolProvider vfxPoolProvider,
            IShipConfig config,
            IBodyTransform transform,
            DeathStateDefinition definition)
        {
            _stateMachine = stateMachine;
            _vfxPoolProvider = vfxPoolProvider;
            _config = config;
            _transform = transform;
            Definition = definition;
        }

        private readonly IStateMachine _stateMachine;
        private readonly IVfxPoolProvider _vfxPoolProvider;
        private readonly IShipConfig _config;
        private readonly IBodyTransform _transform;

        private IObjectProvider<DeadShipVfx> _objectProvider;

        public StateDefinition Definition { get; }

        public void OnAwake()
        {
            _objectProvider = _vfxPoolProvider.GetPool<DeadShipVfx>(_config.DeathVfx);
        }
        
        public void Enter()
        {
            _stateMachine.Enter(this);

            _objectProvider.Get(_transform.Position);

            MessageBroker.Default.Publish(new PlayerDeathEvent());
            MessageBroker.Default.TriggerSound(PositionalSoundType.Death, _transform.Position);
        }

        public void Break()
        {
        }
    }
}