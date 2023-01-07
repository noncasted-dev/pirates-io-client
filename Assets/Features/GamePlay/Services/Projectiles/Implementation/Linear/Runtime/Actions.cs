using System;
using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Mover.Abstract;
using Global.Services.Sounds.Runtime;
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
            IProjectilesMover mover,
            ProjectileType type)
        {
            _returnToPool = returnToPool;
            _droppedCallback = droppedCallback;
            _collidedCallback = collidedCallback;
            _triggeredCallback = triggeredCallback;
            _transform = transform;
            _projectile = projectile;
            _mover = mover;
            Type = type;
        }

        private readonly Action _collidedCallback;
        private readonly Action _droppedCallback;

        private readonly IProjectilesMover _mover;
        private readonly LinearProjectile _projectile;

        private readonly Action<LinearProjectile> _returnToPool;

        private readonly Transform _transform;

        private readonly Action _triggeredCallback;
        private string _creatorId;
        private bool _isLocal;

        private ShootParams _shootParams;

        public bool IsLocal => _isLocal;
        public string CreatorId => _creatorId;
        public ProjectileType Type { get; }

        public void OnTriggered(IDamageReceiver damageReceiver)
        {
            _mover.Remove(_projectile);
            _triggeredCallback?.Invoke();

            var damage = new Damage(
                _shootParams.Damage,
                _transform.position,
                Type);

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

        public void Setup(ShootParams shootParams, bool isLocal, string creatorId)
        {
            _creatorId = creatorId;
            _shootParams = shootParams;
            _isLocal = isLocal;
        }

        public void OnDropped()
        {
            _mover.Remove(_projectile);
            _droppedCallback?.Invoke();

            MessageBrokerSoundExtensions.TriggerSound(PositionalSoundType.ProjectileDropped, _transform.position);
        }
    }
}