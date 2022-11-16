#region

using UnityEngine;

#endregion

namespace GamePlay.Common.Damages
{
    public readonly struct Damage
    {
        public Damage(int amount, Vector2 origin)
        {
            Amount = amount;
            Origin = origin;
        }

        public readonly int Amount;
        public readonly Vector2 Origin;
    }
}