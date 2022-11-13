using System;
using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Mover;
using GamePlay.Services.Projectiles.Mover.Abstract;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Implementation.Linear
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

        [SerializeField] [ReadOnly] private ShootParams _shootParams;
        [SerializeField] [ReadOnly] private bool _isLocal;
        
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private TrailRenderer _trail;
        
        private IProjectilesMover _mover;
        private Movement _movement;
        private Actions _actions;
        private Transform _transform;

        private Action<LinearProjectile> _returnCallback;
        private IObjectProvider<AnimatedVfx> _vfx;
        public IProjectileMovement Movement => _movement;
        public IProjectileActions Actions => _actions;

        public GameObject GameObject => gameObject;

        public void Fire(
            Vector2 direction,
            ShootParams shootParams,
            bool isLocal)
        {
            _shootParams = shootParams;
            _isLocal = isLocal;
            
            var movementData = new MovementData(
                direction,
                shootParams.Speed,
                shootParams.Distance);
            
            var angle = direction.ToAngle();

            _movement.Setup(angle, movementData);
            _actions.Setup(shootParams, isLocal);
            
            _mover.Add(this);
        }

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
                _returnCallback, 
                this,
                _mover, 
                _vfx);
            
            _movement = new Movement(
                raycastData,
                _transform,
                _actions.OnDropped);
        }

        public void OnTaken()
        {
            _trail.Clear();
        }

        public void OnReturned()
        {
            _trail.Clear();
        }
    }
}