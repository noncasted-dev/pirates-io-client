using System;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Entity
{
    [Serializable]
    public class ShootParams
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _pushForce;
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _layerMask;

        public readonly int Damage;
        public readonly LayerMask LayerMask;
        public readonly float PushForce;
        public readonly float Speed;

        public ShootParams(int damage, float pushForce, float speed, LayerMask layerMask)
        {
            Damage = damage;
            PushForce = pushForce;
            Speed = speed;
            LayerMask = layerMask;

            _damage = damage;
            _pushForce = pushForce;
            _speed = speed;
            _layerMask = layerMask;
        }
    }
}