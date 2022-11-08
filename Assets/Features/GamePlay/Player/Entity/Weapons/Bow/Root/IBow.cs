using GamePlay.Player.Entity.Weapons.Common.Root;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Bow.Root
{
    public interface IBow : IWeapon
    {
        void Snap(Vector2 position);
        void Rotate(float angle);
        void SetFlipY(bool isFlipped);
        void Shoot(float angle);
    }
}