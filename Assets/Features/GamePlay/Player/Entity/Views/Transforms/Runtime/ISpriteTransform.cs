using UnityEngine;

namespace GamePlay.Player.Entity.Views.Transforms.Runtime
{
    public interface ISpriteTransform : ITransform
    {
        void Impact(Vector2 direction, float distance, float time);
    }
}