using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.Idles.Logs;
using GamePlay.Player.Entity.Views.Animators.Runtime;

namespace GamePlay.Player.Entity.States.Idles.Runtime
{
    public class Idle : IState, IIdle
    {
        public Idle(
            IStateMachine stateMachine,
            IAnimatorView animator,
            ISpriteRotation spriteRotation,
            IAnimatorRotation animatorRotation,
            StateDefinition definition,
            IdleLogger logger,
            AnimationTrigger animation)
        {
            _stateMachine = stateMachine;
            _animator = animator;
            _spriteRotation = spriteRotation;
            _animatorRotation = animatorRotation;
            Definition = definition;
            _logger = logger;
            _animation = animation;
        }

        private readonly AnimationTrigger _animation;
        private readonly IAnimatorView _animator;
        private readonly IAnimatorRotation _animatorRotation;
        private readonly IdleLogger _logger;
        private readonly ISpriteRotation _spriteRotation;

        private readonly IStateMachine _stateMachine;

        public void Enter()
        {
            _stateMachine.Enter(this);

            _animator.SetTrigger(_animation);
            _animatorRotation.Start();
            _spriteRotation.Start();

            _logger.OnEntered();
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
            _animatorRotation.Stop();
            _spriteRotation.Stop();

            _logger.OnBroke();
        }
    }
}