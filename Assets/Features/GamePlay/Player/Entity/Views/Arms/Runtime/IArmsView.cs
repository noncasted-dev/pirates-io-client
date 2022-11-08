using UnityEngine;

namespace GamePlay.Player.Entity.Views.Arms.Runtime
{
    public interface IArmsView
    {
        void Rotate(float angle);
        void Snap(Vector2 position);
        void SetFlipY(bool isFlipped);
    }
}