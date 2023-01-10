using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Weapons.Cannon.Views.Animators.Runtime
{
    [DisallowMultipleComponent]
    public class CannonAnimator : SubAnimator, IPlayerSwitchListener
    {
        [Inject]
        private void Construct(
            IAnimatorView animatorView)
        {
            _animatorView = animatorView;
        }

        private IAnimatorView _animatorView;

        public void OnEnabled()
        {
            _animatorView.AddSubAnimator(this);
        }

        public void OnDisabled()
        {
            _animatorView.RemoveSubAnimator(this);
        }

        public void OnShootReady()
        {
        }

        public void OnShootPlayed()
        {
        }
    }
}