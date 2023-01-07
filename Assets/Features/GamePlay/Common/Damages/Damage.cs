using UnityEngine;

namespace GamePlay.Common.Damages
{
    public readonly struct Damage
    {
        public Damage(int amount, Vector2 origin, ProjectileType type)
        {
            Amount = amount;
            Origin = origin;
            Type = type;
        }

        public readonly int Amount;
        public readonly Vector2 Origin;
        public readonly ProjectileType Type;
    }
}