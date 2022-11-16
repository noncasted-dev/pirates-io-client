using UnityEngine;

namespace GamePlay.Player.Entity.Views.Pivots.Runtime
{
    public interface IPivots
    {
        Vector2 GetPosition(PivotType type);
    }
}