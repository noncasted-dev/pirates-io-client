using System;
using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Mover;
using GamePlay.Services.Projectiles.Mover.Abstract;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Implementation.Linear.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public class LinearProjectile :
        MonoBehaviour,
        IProjectileStarter,
        IPoolObject<LinearProjectile>,
        IMovableProjectile
    {
        public void Construct(IProjectilesMover mover, IObjectProvider<AnimatedVfx> vfx)
        {
            _vfx = vfx;
            _mover = mover;
            _transform = transform;
        }

        private const int _disableDelay = 3000;

        [SerializeField] [ReadOnly] private ShootParams _shootParams;
        [SerializeField] [ReadOnly] private bool _isLocal;

        [SerializeField] private int _damage = 1;
        [SerializeField] private float _distance = 10f;
        [SerializeField] private float _speed = 20f;
        
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private ProjectileType _type;
        
        private Actions _actions;
        private Movement _movement;

        private IProjectilesMover _mover;

        private Action<LinearProjectile> _returnCallback;
        private Transform _transform;
        private IObjectProvider<AnimatedVfx> _vfx;
        public IProjectileMovement Movement => _movement;
        public IProjectileActions Actions => _actions;

        public GameObject GameObject => gameObject;

        public void SetPosition(Vector2 position)
        {
            _transform.position = position;
        }

        public void SetupPoolObject(Action<LinearProjectile> returnToPool)
        {
            _returnCallback = returnToPool;

            var raycastData = new ProjectileRaycastData(_collider.size.y);

            _actions = new Actions(
                _transform,
                this,
                returnToPool,
                OnTriggered,
                OnCollided,
                OnDropped,
                _mover,
                _type);

            _movement = new Movement(
                raycastData,
                _transform,
                _actions.OnDropped);
        }
        
        public void Fire(
            Vector2 direction,
            ShootParams shootParams,
            bool isLocal,
            string creatorId)
        {
            shootParams = new ShootParams(_damage, _speed, _distance);
            
            _shootParams = shootParams;
            _isLocal = isLocal;

            var movementData = new MovementData(
                direction,
                shootParams.Speed,
                shootParams.Distance);

            var angle = direction.ToAngle();

            _movement.Setup(angle, movementData);
            _actions.Setup(shootParams, isLocal, creatorId);
            _collider.enabled = true;

            _mover.Add(this);

            _spriteRenderer.enabled = true;
        }

        public void OnTaken()
        {
            _trail.Clear();
        }

        public void OnReturned()
        {
            _trail.Clear();
        }

        private void OnCollided() => OnCollidedAsync().Forget();
        private void OnTriggered() => OnTriggeredAsync().Forget();
        private void OnDropped() => OnDroppedAsync().Forget();

        private async UniTaskVoid OnCollidedAsync()
        {
            _spriteRenderer.enabled = false;
            _particleSystem.Stop();
            _collider.enabled = false;
            
            var cancellation = this.GetCancellationTokenOnDestroy();
            await UniTask.Delay(_disableDelay, false, PlayerLoopTiming.Update, cancellation);
            
            _returnCallback?.Invoke(this);
        }

        private async UniTaskVoid OnTriggeredAsync()
        {
            _spriteRenderer.enabled = false;
            _particleSystem.Stop();
            _collider.enabled = false;

            var cancellation = this.GetCancellationTokenOnDestroy();
            await UniTask.Delay(_disableDelay, false, PlayerLoopTiming.Update, cancellation);
            
            _returnCallback?.Invoke(this);
        }

        private async UniTaskVoid OnDroppedAsync()
        {
            _spriteRenderer.enabled = false;
            _particleSystem.Stop();
            _vfx.Get(transform.position);
            _collider.enabled = false;

            var cancellation = this.GetCancellationTokenOnDestroy();
            await UniTask.Delay(_disableDelay, false, PlayerLoopTiming.Update, cancellation);
            
            _returnCallback?.Invoke(this);
        }
    }
}