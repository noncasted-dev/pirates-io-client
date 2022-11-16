#region

using System;
using System.Collections.Generic;
using GamePlay.Player.Entity.Network.Views.Transforms.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.RigidBodies.Logs;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

#endregion

namespace GamePlay.Player.Entity.Views.RigidBodies.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerRigidBody : MonoBehaviour,
        IRigidBody,
        IAwakeCallback,
        ISwitchCallbacks,
        IFixedUpdatable,
        IUpdatable
    {
        [Inject]
        private void Construct(
            ILogger logger,
            IUpdater updater,
            INetworkTransform networkTransform)
        {
            _networkTransform = networkTransform;
            _logger = new RigidBodyLogger(logger, _logSettings);
            _updater = updater;
        }

        [SerializeField] private RigidBodyLogSettings _logSettings;

        private readonly Queue<PhysicsInteraction> _interactions = new();

        private readonly Queue<PhysicsMove> _moves = new();
        private readonly Queue<Vector2> _teleports = new();

        private Vector2 _currentPosition;
        private RigidBodyLogger _logger;
        private INetworkTransform _networkTransform;

        private Rigidbody2D _rigidbody;
        private IUpdater _updater;

        public Vector2 Position => _rigidbody.position;

        public void OnAwake()
        {
            _rigidbody = GetComponentInParent<Rigidbody2D>();

            _currentPosition = transform.position;
        }

        public void OnFixedUpdate(float delta)
        {
            foreach (var interaction in _interactions)
                switch (interaction)
                {
                    case PhysicsInteraction.Move:
                        var move = _moves.Dequeue();
                        _currentPosition = ProcessMove(move.Direction, move.Distance);

                        _logger.OnMoveProcessed(move.Direction, move.Distance, _currentPosition);
                        break;
                    case PhysicsInteraction.Teleport:
                        _currentPosition = _teleports.Dequeue();

                        _logger.OnPositionSet(_currentPosition);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            _rigidbody.MovePosition(_currentPosition);

            _interactions.Clear();
        }

        public void SetPosition(Vector2 position)
        {
            _teleports.Enqueue(position);
            _interactions.Enqueue(PhysicsInteraction.Teleport);
        }

        public void Move(Vector2 direction, float distance)
        {
            var move = new PhysicsMove(direction, distance);

            _moves.Enqueue(move);
            _interactions.Enqueue(PhysicsInteraction.Move);

            _logger.OnMoveEnqueued(direction, distance);
        }

        public void OnEnabled()
        {
            _updater.Add((IFixedUpdatable)this);
            _updater.Add((IUpdatable)this);
        }

        public void OnDisabled()
        {
            _updater.Remove((IFixedUpdatable)this);
            _updater.Remove((IUpdatable)this);
        }

        private Vector2 ProcessMove(Vector2 direction, float distance)
        {
            return _currentPosition + direction * distance;
        }

        public void OnUpdate(float delta)
        {
            _currentPosition = _rigidbody.position;
            _networkTransform.SetPosition(_currentPosition);
        }
    }
}