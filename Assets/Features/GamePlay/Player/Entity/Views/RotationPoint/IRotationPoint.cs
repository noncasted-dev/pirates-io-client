using UnityEngine;

namespace GamePlay.Player.Entity.Views.RotationPoint
{
    public interface IRotationPoint
    {
        Vector2 Position { get; }
        Transform Transform { get; }
    }
}