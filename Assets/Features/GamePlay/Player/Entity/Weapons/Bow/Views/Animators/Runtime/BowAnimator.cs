using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Weapons.Bow.Views.Animators.Runtime
{
    [DisallowMultipleComponent]
    public class BowAnimator : SubAnimator, ISwitchCallbacks
    {
        [Inject]
        private void Construct(
            IAnimatorView animatorView,
            RangeAttackAnimatorCallbackBridge callbackBridge)
        {
            _callbackBridge = callbackBridge;
            _animatorView = animatorView;
        }

        private IAnimatorView _animatorView;
        private RangeAttackAnimatorCallbackBridge _callbackBridge;

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
            _callbackBridge.OnShootReady();
        }

        public void OnShootPlayed()
        {
            _callbackBridge.OnPlayed();
        }
    }
}