using UnityEngine;

namespace GamePlay.Player.Entity.Views.Sprites.Runtime
{
    public interface ISpriteFlipper
    {
        void ResetRotation();
        void SetFlipX(bool isFlipped, bool flipSubSprites);
        void FlipAlong(Vector2 direction, bool flipSubSprites);
    }
}