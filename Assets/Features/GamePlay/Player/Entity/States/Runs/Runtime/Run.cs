using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.Runs.Logs;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Runs.Runtime
{
    public class Run : IRun, IState, IPreFixedUpdatable
    {
        public Run(
            IStateMachine stateMachine,
            IRigidBody rigidBody,
            IUpdater updater,
            IRunConfig runConfig,
            ISpriteFlipper spriteFlipper,
            StateDefinition definition,
            RunLogger logger)
        {
            _stateMachine = stateMachine;
            _rigidBody = rigidBody;
            _updater = updater;
            _runConfig = runConfig;

            _spriteFlipper = spriteFlipper;

            Definition = definition;
            _logger = logger;
        }

        private readonly RunLogger _logger;
        private readonly IRigidBody _rigidBody;
        private readonly IRunConfig _runConfig;

        private readonly ISpriteFlipper _spriteFlipper;

        private readonly IStateMachine _stateMachine;
        private readonly IUpdater _updater;

        private Vector2 _input;
        private bool _isStarted;

        public void OnPreFixedUpdate(float delta)
        {
            if (_isStarted == false)
                Debug.LogError("Not started");

            _rigidBody.Move(_input, _runConfig.Speed * delta);
        }

        public bool HasInput => _input != Vector2.zero;

        public void OnInput(Vector2 input)
        {
            _input = input;

            if (_isStarted == true)
            {
                UpdateInput();
                return;
            }

            if (_stateMachine.IsAvailable(Definition) == false)
                return;

            _logger.OnEnteredOnTrigger(input);

            Begin();
        }

        public void OnCancel()
        {
            _input = Vector2.zero;

            if (_isStarted == false)
                return;

            _stateMachine.Exit();
        }

        public void Enter()
        {
            if (_input == Vector2.zero)
            {
                _logger.OnEnterFromFloatingError();
                return;
            }

            _logger.OnEnteredFromFloating(_input);

            Begin();
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
            if (_isStarted == false)
                return;

            _isStarted = false;

            _updater.Remove(this);

            _logger.OnBroke();
        }

        private void Begin()
        {
            UpdateInput();

            if (_isStarted == true)
                return;

            _isStarted = true;
            _stateMachine.Enter(this);
            _updater.Add(this);
        }

        private void UpdateInput()
        {
            _spriteFlipper.FlipAlong(_input, true);
        }
    }
}