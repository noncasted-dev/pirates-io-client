using System;
using Features.GamePlay.Services.PlayerPaths.Builder.Runtime;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace Features.GamePlay.Player.Entity.States.PathFollowing.Runtime
{
    public class PathFollower : IState, IPlayerSwitchListener
    {
        public PathFollower(
            IStateMachine stateMachine,
            ISpriteRotation spriteRotation,
            IRigidBody rigidBody,
            ISpeedCalculator speedCalculator,
            IUpdater updater,
            PathFollowDefinition definition)
        {
            _stateMachine = stateMachine;
            _spriteRotation = spriteRotation;
            _rigidBody = rigidBody;
            _speedCalculator = speedCalculator;
            _updater = updater;

            Definition = definition;
        }

        private readonly ISpriteRotation _spriteRotation;
        private readonly IRigidBody _rigidBody;
        private readonly ISpeedCalculator _speedCalculator;
        private readonly IUpdater _updater;
        private readonly IStateMachine _stateMachine;

        private PathMovement _current;
        private IDisposable _pathListener;

        public StateDefinition Definition { get; }

        public void OnEnabled()
        {
            _pathListener = Msg.Listen<PlayerPathBuildEvent>(Follow);
        }

        public void OnDisabled()
        {
            _pathListener?.Dispose();
        }
        
        private void Follow(PlayerPathBuildEvent data)
        {
            _stateMachine.Enter(this);
            
            _current?.Stop();
            
            _current = new PathMovement(_updater, _rigidBody, _speedCalculator, data.Path);
            _current.Start();
            _spriteRotation.Start();
        }
        
        public void Break()
        {
            _spriteRotation.Stop();
            _current?.Stop();
        }
    }
}