using UnityEngine;

namespace Global.Services.InputViews.Runtime
{
    public readonly struct LineResult
    {
        public LineResult(Vector2 direction, float length)
        {
            Direction = direction;
            Length = length;
        }

        public readonly Vector2 Direction;
        public readonly float Length;
    }
}