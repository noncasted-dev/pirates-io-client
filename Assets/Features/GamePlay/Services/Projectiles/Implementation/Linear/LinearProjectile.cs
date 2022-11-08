using System;
using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Mover;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Implementation.Linear
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public class LinearProjectile : MonoBehaviour, IProjectile, IPoolObject<LinearProjectile>
    {
        public void Construct(IProjectilesMover mover)
        {
            _mover = mover;
        }

        [SerializeField] [ReadOnly] private ShootParams _shootParams;

        private float _angle;
        private BoxCollider2D _collider;
        private Vector2 _direction;

        private IProjectilesMover _mover;
        private Vector2 _position;

        private Action<LinearProjectile> _returnCallback;
        private Transform _transform;

        public GameObject GameObject => gameObject;

        public void SetupPoolObject(Action<LinearProjectile> returnToPool)
        {
            _returnCallback = returnToPool;

            _collider = GetComponent<BoxCollider2D>();
        }

        public void OnTaken()
        {
        }

        public void OnReturned()
        {
        }

        public LayerMask LayerMask => _shootParams.LayerMask;
        public Vector2 Position => _position;
        public Vector2 Direction => _direction;
        public float ColliderHeight => _collider.size.y;
        public float Angle => _angle;
        public float Speed => _shootParams.Speed;

        public void Fire(
            Vector2 position,
            Vector2 direction,
            ShootParams shootParams)
        {
            _shootParams = shootParams;
            _position = position;
            _direction = direction;
            _angle = _direction.GetAngle();

            transform.rotation = Quaternion.Euler(0f, 0f, _angle);

            _mover.Add(this);
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
            transform.position = position;
        }

        public void OnCollided()
        {
            _mover.Remove(this);
            _returnCallback?.Invoke(this);
        }

        public void OnTriggered(IDamageReceiver damageReceiver)
        {
            var damage = new Damage(_shootParams.Damage, _shootParams.PushForce, Position);
            damageReceiver.ReceiveDamage(damage);

            _mover.Remove(this);
            _returnCallback?.Invoke(this);
        }
    }
}