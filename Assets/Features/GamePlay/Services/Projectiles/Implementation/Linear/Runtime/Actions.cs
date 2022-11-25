using System;
using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Mover.Abstract;
using Global.Services.Sounds.Runtime;
using UniRx;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Implementation.Linear.Runtime
{
    public class Actions : IProjectileActions
    {
        public Actions(
            Transform transform,
            LinearProjectile projectile,
            Action<LinearProjectile> returnToPool,
            Action triggeredCallback,
            Action collidedCallback,
            Action droppedCallback,
            IProjectilesMover mover)
        {
            _returnToPool = returnToPool;
            _droppedCallback = droppedCallback;
            _collidedCallback = collidedCallback;
            _triggeredCallback = triggeredCallback;
            _transform = transform;
            _projectile = projectile;
            _mover = mover;
        }

        private readonly IProjectilesMover _mover;
        private readonly LinearProjectile _projectile;

        private readonly Transform _transform;
        private string _creatorId;
        private bool _isLocal;

        private ShootParams _shootParams;
        
        private readonly Action _triggeredCallback;
        private readonly Action _collidedCallback;
        private readonly Action _droppedCallback;
        
        private readonly Action<LinearProjectile> _returnToPool;

        public bool IsLocal => _isLocal;
        public string CreatorId => _creatorId;

        public void Setup(ShootParams shootParams, bool isLocal, string creatorId)
        {
            _creatorId = creatorId;
            _shootParams = shootParams;
            _isLocal = isLocal;
        }
        
        public void OnTriggered(IDamageReceiver damageReceiver)
        {
            _mover.Remove(_projectile);
            _triggeredCallback?.Invoke();

            var damage = new Damage(
                _shootParams.Damage,
                _transform.position);

            damageReceiver.ReceiveDamage(damage, IsLocal);
        }

        public void Destroy()
        {
            _mover.Remove(_projectile);
            _returnToPool?.Invoke(_projectile);
        }

        public void OnCollided()
        {
            _mover.Remove(_projectile);
            _collidedCallback?.Invoke();
        }

        public void OnDropped()
        {
            _mover.Remove(_projectile);
            _droppedCallback?.Invoke();
            
            MessageBroker.Default.TriggerSound(PositionalSoundType.ProjectileDropped, _transform.position);
        }
    }
}