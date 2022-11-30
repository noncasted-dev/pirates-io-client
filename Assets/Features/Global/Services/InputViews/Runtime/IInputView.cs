using System;
using UnityEngine;

namespace Global.Services.InputViews.Runtime
{
    public interface IInputView
    {
        event Action<Vector2> MovementPerformed;
        event Action MovementCanceled;

        event Action RangeAttackPerformed;
        event Action RangeAttackCanceled;
        event Action RangeAttackBreakPerformed;
        event Action InventoryPerformed;
        event Action MapPerformed;
        event Action SelectFirstProjectilePerformed;
        event Action SelectSecondProjectilePerformed;
        event Action SelectThirdProjectilePerformed;
        event Action SelectForthProjectilePerformed;

        event Action DebugConsolePreformed;

        float GetAngleFrom(Vector2 from);
        Vector2 GetDirectionFrom(Vector2 from);
        LineResult GetLineFrom(Vector2 from);
    }
}