#region

using UnityEngine;

#endregion

namespace Common.Structs
{
    public static class DirectionUtils
    {
        public static float ToAngle(this Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < 0)
                angle += 360f;

            return angle;
        }

        public static bool IsVertical(this Vector2 direction)
        {
            if (Mathf.Approximately(direction.x, 0f) == false)
                return false;

            if (Mathf.Approximately(direction.y, 0f) == true)
                return false;

            return true;
        }

        public static bool IsZero(this Vector2 direction)
        {
            if (Mathf.Approximately(direction.x, 0f) == false)
                return false;

            if (Mathf.Approximately(direction.y, 0f) == false)
                return false;

            return true;
        }

        public static bool IsAlong(this Vector2 a, Vector2 b)
        {
            if (a.x > 0 && b.x > 0)
                return true;

            if (a.x < 0 && b.x < 0)
                return true;

            if (a.y > 0 && b.y > 0)
                return true;

            if (a.y < 0 && b.y < 0)
                return true;

            return false;
        }

        public static Horizontal ToHorizontal(float x)
        {
            if (x < 0)
                return Horizontal.Left;

            return Horizontal.Right;
        }
    }
}