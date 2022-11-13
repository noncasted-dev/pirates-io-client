using System;
using System.Collections.Generic;
using GamePlay.Player.Entity.Network.Views.Transforms.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.RigidBodies.Logs;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Views.RigidBodies.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerRigidBody : MonoBehaviour,
        IRigidBody,
        IAwakeCallback,
        ISwitchCallbacks,
        IFixedUpdatable
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
        [SerializeField] private LayerMask _collideMask;

        private readonly RaycastHit2D[] _buffer = new RaycastHit2D[1];
        private readonly Queue<PhysicsInteraction> _interactions = new();

        private readonly Queue<PhysicsMove> _moves = new();
        private readonly Queue<Vector2> _teleports = new();

        private CapsuleCollider2D _collider;

        private Vector2 _currentPosition;
        private RigidBodyLogger _logger;
        private INetworkTransform _networkTransform;

        private Rigidbody2D _rigidbody;
        private IUpdater _updater;

        public void OnAwake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();

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

            _networkTransform.SetPosition(_currentPosition);

            _interactions.Clear();
        }

        public Vector2 Position => _rigidbody.position;

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
            _updater.Add(this);
        }

        public void OnDisabled()
        {
            _updater.Remove(this);
        }

        private Vector2 ProcessMove(Vector2 direction, float distance)
        {
            var resultPosition = _currentPosition;

            if (Mathf.Approximately(direction.x, 0f) == false)
            {
                var xPosition = _currentPosition;
                xPosition.x += direction.x * distance;

                var resultX = Physics2D.CapsuleCastNonAlloc(
                    xPosition,
                    _collider.size,
                    _collider.direction,
                    0f,
                    direction,
                    _buffer,
                    distance,
                    _collideMask);

                if (resultX == 0)
                    resultPosition.x += direction.x * distance;
                else
                    resultPosition.x += direction.x * _buffer[0].distance;
            }

            if (Mathf.Approximately(direction.y, 0f) == false)
            {
                var yPosition = resultPosition;
                yPosition.y += direction.y * distance;

                var resultY = Physics2D.CapsuleCastNonAlloc(
                    yPosition,
                    _collider.size,
                    _collider.direction,
                    0f,
                    direction,
                    _buffer,
                    distance,
                    _collideMask);

                if (resultY == 0)
                    resultPosition.y += direction.y * distance;
                else
                    resultPosition.y += direction.y * _buffer[0].distance;
            }

            return resultPosition;
        }
    }
}