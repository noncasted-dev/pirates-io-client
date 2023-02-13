using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using Global.Services.MessageBrokers.Runtime;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Deaths.Runtime
{
    public class BotDeath : IDeath, IState
    {
        public BotDeath(
            IStateMachine stateMachine,
            DeathStateDefinition definition,
            LocalPlayerRoot root,
            IDroppedObjectsPresenter droppedObjectsPresenter,
            IBodyTransform transform
        )
        {
            _stateMachine = stateMachine;
            _root = root;
            _droppedObjectsPresenter = droppedObjectsPresenter;
            _transform = transform;
            Definition = definition;
        }

        private readonly IDroppedObjectsPresenter _droppedObjectsPresenter;
        private readonly LocalPlayerRoot _root;

        private readonly IStateMachine _stateMachine;
        private readonly IBodyTransform _transform;

        private bool _isDead = false;

        public void Enter()
        {
            if (_isDead == true)
                return;

            _isDead = true;

            _stateMachine.Enter(this);

            _droppedObjectsPresenter.DropFromDeath(ItemType.Cannon, Random.Range(1, 10), _transform.Position);
            _droppedObjectsPresenter.DropFromDeath(ItemType.CannonShrapnel, Random.Range(1, 10), _transform.Position);
            _droppedObjectsPresenter.DropFromDeath(ItemType.CannonKnuppel, Random.Range(1, 10), _transform.Position);

            for (var i = 0; i < 7; i++)
                _droppedObjectsPresenter.DropFromDeath((ItemType)Random.Range(18, 30), Random.Range(1, 3),
                    _transform.Position);

            Msg.Publish(new BotDeathEvent());
            RagonNetwork.Room.DestroyEntity(_root.gameObject);
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
        }
    }
}