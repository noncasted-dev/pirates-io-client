using System;
using System.Collections;
using System.Collections.Generic;
using Common.ObjectsPools.Runtime.Abstract;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace GamePlay.Services.VFX.Pool.Implementation.Animated
{
    [DisallowMultipleComponent]
   // [RequireComponent(typeof(Animator))]
    public class AnimatedVfx : MonoBehaviour, IPoolObject<AnimatedVfx>
    {
        [SerializeField] private List<GameObject> _skinVariants;
        [SerializeField] private float _duration;
        [SerializeField] private UnityEvent _callOnEnd;
        private static readonly int Play = Animator.StringToHash("Play");

        private Animator _animator;
        private Action<AnimatedVfx> _returnToPool;

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
            if (_skinVariants.Count > 0)
            {
                var randomId = (int)(Random.value * (float)_skinVariants.Count);
                for (var i = 0; i < _skinVariants.Count; i++)
                    _skinVariants[i].SetActive(randomId == i);
            }
            else
            {
                if (_animator)
                    _animator.SetTrigger(Play);
            }

            StartCoroutine(DelayCallEnd());
            IEnumerator DelayCallEnd()
            {
                yield return new WaitForSeconds(_duration);
                _callOnEnd.Invoke();
            }
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