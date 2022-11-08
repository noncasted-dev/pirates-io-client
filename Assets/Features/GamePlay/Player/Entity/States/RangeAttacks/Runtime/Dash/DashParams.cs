using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Dash
{
    public class DashParams
    {
        public DashParams(float distance, float time, AnimationCurve curve)
        {
            Distance = distance;
            Time = time;
            _curve = curve;
        }

        private readonly AnimationCurve _curve;

        public readonly float Distance;
        public readonly float Time;

        public float Evaluate(float time)
        {
            return _curve.Evaluate(time / Time);
        }
    }
}