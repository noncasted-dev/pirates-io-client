using System;
using System.Collections.Generic;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace GamePlay.Services.VFX.Pool.Implementation.Dead
{
    [DisallowMultipleComponent]
    public class DeadShipVfx : MonoBehaviour, IPoolObject<DeadShipVfx>
    {
        [SerializeField] private float _destroyDelay;
        [SerializeField] private float _disappearDelay;

        [SerializeField] private List<GameObject> _skinVariants;
        [SerializeField] private UnityEvent _disappearStartedCallback;
        
        private static readonly int Play = Animator.StringToHash("Play");

        private Animator _animator;
        private Action<DeadShipVfx> _returnToPool;

        public GameObject GameObject => gameObject;

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetupPoolObject(Action<DeadShipVfx> returnToPool)
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
        }

        public void OnReturned()
        {
        }

        private async UniTaskVoid ReturnOnDelay()
        {
            var destroyDelay = (int)(_destroyDelay * 1000);
            var token = this.GetCancellationTokenOnDestroy();
            
            await UniTask.Delay(destroyDelay, false, PlayerLoopTiming.Update, token);
            
            var disappearDelay = (int)(_disappearDelay * 1000);
            _disappearStartedCallback?.Invoke();

            await UniTask.Delay(destroyDelay, false, PlayerLoopTiming.Update, token);
            
            _returnToPool?.Invoke(this);
            
            _returnToPool?.Invoke(this);
        }
    }
}