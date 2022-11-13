using UnityEngine;

namespace GamePlay.Player.Entity.Views.ShootPoint
{
    public interface IShootPoint
    {
        Vector2 GetShootPoint(float angle);
    }
}