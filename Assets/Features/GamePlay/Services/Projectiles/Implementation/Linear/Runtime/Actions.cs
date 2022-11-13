using System;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Mover.Abstract;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Implementation.Linear
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

        private readonly Transform _transform;
        private readonly Action<LinearProjectile> _returnToPool;
        private readonly LinearProjectile _projectile;
        private readonly IProjectilesMover _mover;
        private readonly IObjectProvider<AnimatedVfx> _waterSplash;

        private ShootParams _shootParams;
        private bool _isLocal;

        public bool IsLocal => _isLocal;
        
        public void Setup(ShootParams shootParams, bool isLocal)
        {
            _shootParams = shootParams;
            _isLocal = isLocal;
        }

        public void OnTriggered(IDamageReceiver damageReceiver)
        {
            _mover.Remove(_projectile);
            _returnToPool?.Invoke(_projectile);
            
            if (_isLocal == false)
                return;
            
            var damage = new Damage(
                _shootParams.Damage,
                _transform.position);

            damageReceiver.ReceiveDamage(damage);
        }

        public void OnCollided()
        {
            _mover.Remove(_projectile);
            _returnToPool?.Invoke(_projectile);
        }

        public void OnDropped()
        {
            _mover.Remove(_projectile);
            _returnToPool?.Invoke(_projectile);
            _waterSplash.Get(_transform.position);
        }
    }
}