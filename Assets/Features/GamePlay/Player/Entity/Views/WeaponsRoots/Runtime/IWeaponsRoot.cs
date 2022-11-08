using UnityEngine;

namespace GamePlay.Player.Entity.Views.WeaponsRoots.Runtime
{
    public interface IWeaponsRoot
    {
        Transform Transform { get; }
        Vector2 Position { get; }
    }
}