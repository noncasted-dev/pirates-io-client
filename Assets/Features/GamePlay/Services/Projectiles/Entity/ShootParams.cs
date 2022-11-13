using System;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Entity
{
    [Serializable]
    public class ShootParams
    {
        public ShootParams(
            int damage,
            float pushForce,
            float speed,
            float distance,
            LayerMask layerMask)
        {
            Damage = damage;
            PushForce = pushForce;
            Speed = speed;
            LayerMask = layerMask;
            Distance = distance;

            _damage = damage;
            _pushForce = pushForce;
            _speed = speed;
            _distance = distance;
            _layerMask = layerMask;
        }
        
        [SerializeField] private int _shotsAmount;
        [SerializeField] private int _damage;
        [SerializeField] private float _pushForce;
        [SerializeField] private float _speed;
        [SerializeField] private float _distance;
        [SerializeField] private LayerMask _layerMask;

        public readonly int Damage;
        public readonly LayerMask LayerMask;
        public readonly float PushForce;
        public readonly float Speed;
        public readonly float Distance;
    }
}