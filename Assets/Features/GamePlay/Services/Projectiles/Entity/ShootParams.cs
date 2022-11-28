using System;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Entity
{
    [Serializable]
    public class ShootParams
    {
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
        }
    }
}