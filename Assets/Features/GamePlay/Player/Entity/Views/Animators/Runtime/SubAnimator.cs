using UnityEngine;

namespace GamePlay.Player.Entity.Views.Animators.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public class SubAnimator : MonoBehaviour
    {
        private Animator _animator;

        public void OnAwake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Trigger(int trigger)
        {
            _animator.SetTrigger(trigger);
        }

        public void SetSpeed(float speed)
        {
            _animator.speed = speed;
        }

        public void SetFloat(int hash, float value)
        {
            _animator.SetFloat(hash, value);
        }
    }
}