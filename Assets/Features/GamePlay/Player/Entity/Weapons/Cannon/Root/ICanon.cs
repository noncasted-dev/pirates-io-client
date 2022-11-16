using GamePlay.Player.Entity.Weapons.Common.Root;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Cannon.Root
{
    public interface ICanon : IWeapon
    {
        void Snap(Vector2 position);
        void Rotate(float angle);
        void SetFlipY(bool isFlipped);
        void CancelShoot();
        void Shoot(float angle, float spread);
    }
}