#region

using UnityEngine;

#endregion

namespace Common.Structs
{
    public static class AngleUtils
    {
        public static Horizontal ToHorizontal(float angle)
        {
            if (angle is > 90f and < 270f)
                return Horizontal.Left;

            return Horizontal.Right;
        }

        public static void RotateAlong(this Transform transform, Vector2 direction)
        {
            var angle = direction.ToAngle();
            var rotation = Quaternion.Euler(0f, 0f, angle);

            transform.rotation = rotation;
        }

        public static Vector2 ToDirection(float angle)
        {
            var radians = angle * Mathf.Deg2Rad;
            var x = Mathf.Cos(radians);
            var y = Mathf.Sin(radians);
            var direction = new Vector2(x, y);

            return direction;
        }
    }
}