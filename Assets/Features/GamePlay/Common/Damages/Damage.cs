using UnityEngine;

namespace GamePlay.Common.Damages
{
    public readonly struct Damage
    {
        public Damage(int amount, float pushForce, Vector2 origin)
        {
            Amount = amount;
            PushForce = pushForce;
            Origin = origin;
        }

        public readonly int Amount;
        public readonly float PushForce;
        public readonly Vector2 Origin;
    }
}