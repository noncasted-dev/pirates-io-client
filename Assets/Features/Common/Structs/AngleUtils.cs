using UnityEngine;

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