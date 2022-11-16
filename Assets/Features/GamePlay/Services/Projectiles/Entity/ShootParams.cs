using System;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Entity
{
    [Serializable]
    public class ShootParams
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _distance;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _pushForce;
        [SerializeField] private int _shotsAmount;
        [SerializeField] private float _speed;

        public readonly int Damage;
        public readonly float Distance;
        public readonly float Speed;

        public ShootParams(
            int damage,
            float speed,
            float distance)
        {
            Damage = damage;
            Speed = speed;
            Distance = distance;

            _damage = damage;
            _speed = speed;
            _distance = distance;
        }
    }
}