using GamePlay.Player.Entity.Components.Rotations.Logs;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using Global.Services.Updaters.Runtime.Abstract;

namespace GamePlay.Player.Entity.Components.Rotations.Runtime
{
    public class AnimatorRotation : IUpdatable, IAnimatorRotation
    {
        public AnimatorRotation(
            IRotation rotation,
            IAnimatorView animator,
            IUpdater updater,
            AnimatorFloat animatorFloat,
            RotationLogger logger)
        {
            _rotation = rotation;
            _animator = animator;
            _updater = updater;
            _animatorFloat = animatorFloat;
            _logger = logger;
        }

        private readonly IAnimatorView _animator;
        private readonly AnimatorFloat _animatorFloat;
        private readonly RotationLogger _logger;

        private readonly IRotation _rotation;
        private readonly IUpdater _updater;

        public void Rotate()
        {
            _logger.OnAnimatorRotated(_rotation.Angle);
            _animator.SetFloat(_animatorFloat, _rotation.Angle);
        }

        public void Rotate(float angle)
        {
            _logger.OnAnimatorRotated(angle);
            _animator.SetFloat(_animatorFloat, angle);
        }

        public void Start()
        {
            _updater.Add(this);

            OnUpdate();
        }

        public void Stop()
        {
            _updater.Remove(this);
        }

        public void OnUpdate(float delta = 0f)
        {
            _logger.OnAnimatorRotated(_rotation.Angle);
            _animator.SetFloat(_animatorFloat, _rotation.Angle);
        }
    }
}