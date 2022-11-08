using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack
{
    public interface IRangeAttackRotator
    {
        void Rotate(Vector2 direction);

        void ToDefault();
    }
}