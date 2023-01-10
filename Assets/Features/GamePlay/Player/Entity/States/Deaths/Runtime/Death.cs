using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.Views.ShipConfig.Runtime;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Dead;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Sounds.Runtime;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Deaths.Runtime
{
    public class Death : IDeath, IState, IPlayerAwakeListener
    {
        public Death(
            IStateMachine stateMachine,
            IVfxPoolProvider vfxPoolProvider,
            IShipConfig config,
            IBodyTransform transform,
            IDroppedObjectsPresenter droppedObjectsPresenter,
            PlayerNetworkRoot root,
            DeathStateDefinition definition)
        {
            _stateMachine = stateMachine;
            _vfxPoolProvider = vfxPoolProvider;
            _config = config;
            _transform = transform;
            _droppedObjectsPresenter = droppedObjectsPresenter;
            _root = root;
            Definition = definition;
        }

        private readonly IShipConfig _config;
        private readonly IDroppedObjectsPresenter _droppedObjectsPresenter;
        private readonly PlayerNetworkRoot _root;

        private readonly IStateMachine _stateMachine;
        private readonly IBodyTransform _transform;
        private readonly IVfxPoolProvider _vfxPoolProvider;

        private bool _isDead = false;

        private IObjectProvider<DeadShipVfx> _objectProvider;

        public void OnAwake()
        {
            _objectProvider = _vfxPoolProvider.GetPool<DeadShipVfx>(_config.DeathVfx);
        }

        public void Enter()
        {
            if (_isDead == true)
                return;


            if (_root.IsMine == false)
            {
                _isDead = true;

                _stateMachine.Enter(this);

                _objectProvider.Get(_transform.Position);

                Msg.Publish(new PlayerDeathEvent());
                MessageBrokerSoundExtensions.TriggerSound(PositionalSoundType.Death, _transform.Position);
            }
            else
            {
                _isDead = true;

                _stateMachine.Enter(this);

                _droppedObjectsPresenter.DropFromDeath(ItemType.Cannon, Random.Range(1, 10), _transform.Position);
                _droppedObjectsPresenter.DropFromDeath(ItemType.CannonShrapnel, Random.Range(1, 10),
                    _transform.Position);
                _droppedObjectsPresenter.DropFromDeath(ItemType.CannonKnuppel, Random.Range(1, 10),
                    _transform.Position);

                for (var i = 0; i < 7; i++)
                    _droppedObjectsPresenter.DropFromDeath((ItemType)Random.Range(18, 30), Random.Range(1, 3),
                        _transform.Position);

                Msg.Publish(new BotDeathEvent());
                RagonNetwork.Room.DestroyEntity(_root.gameObject);
            }
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
        }
    }
}