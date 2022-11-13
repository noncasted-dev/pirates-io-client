using Common.Structs;
using UnityEngine;

namespace GamePlay.Player.Entity.Views.Transforms.Runtime
{
    public interface ITransform
    {
        Vector2 Position { get; }
        float Rotation { get; }
        Horizontal Look { get; }

        void SetPosition(Vector2 position);
        void SetLocalPosition(Vector2 position);
        void SetRotation(float angle);
    }
}