using System;
using Common.ObjectsPools.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Services.VFX.Pool.Implementation.Animated
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public class AnimatedVfx : MonoBehaviour, IPoolObject<AnimatedVfx>
    {
        private Action<AnimatedVfx> _returnToPool;

        private Animator _animator;
        
        private static readonly int Play = Animator.StringToHash("Play");

        public GameObject GameObject => gameObject;
        
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetupPoolObject(Action<AnimatedVfx> returnToPool)
        {
            _returnToPool = returnToPool;
            _animator = GetComponent<Animator>();
        }

        public void OnTaken()
        {
            _animator.SetTrigger(Play);
        }

        public void OnReturned()
        {
        }

        public void OnAnimationPlayed()
        {
            _returnToPool?.Invoke(this);
        }
    }
}