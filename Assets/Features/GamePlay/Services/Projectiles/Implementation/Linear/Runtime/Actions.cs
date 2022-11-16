using System;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Mover.Abstract;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Implementation.Linear.Runtime
{
    public class Actions : IProjectileActions
    {
        public Actions(
            Transform transform,
            Action<LinearProjectile> returnToPool,
            LinearProjectile projectile,
            IProjectilesMover mover,
            IObjectProvider<AnimatedVfx> waterSplash)
        {
            _transform = transform;
            _returnToPool = returnToPool;
            _projectile = projectile;
            _mover = mover;
            _waterSplash = waterSplash;
        }

        private readonly IProjectilesMover _mover;
        private readonly LinearProjectile _projectile;
        private readonly Action<LinearProjectile> _returnToPool;

        private readonly Transform _transform;
        private readonly IObjectProvider<AnimatedVfx> _waterSplash;
        private string _creatorId;
        private bool _isLocal;

        private ShootParams _shootParams;

        public bool IsLocal => _isLocal;
        public string CreatorId => _creatorId;

        public void OnTriggered(IDamageReceiver damageReceiver)
        {
            _mover.Remove(_projectile);
            _returnToPool?.Invoke(_projectile);

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
            _returnToPool?.Invoke(_projectile);
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
            _returnToPool?.Invoke(_projectile);
            _waterSplash.Get(_transform.position);
        }
    }
}