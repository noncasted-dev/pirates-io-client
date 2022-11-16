#region

using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Components.InertialMovements.Runtime
{
    public class InertiaCurve
    {
        public InertiaCurve(InertialMovementConfigAsset config)
        {
            _config = config;
        }

        private readonly InertialMovementConfigAsset _config;

        public float Evaluate(float currentTime, Vector2 startDirection, Vector2 targetDirection)
        {
            var distance = Vector2.Distance(startDirection, targetDirection);

            var time = distance * _config.LerpDistanceMultiplier * _config.LerpTime;

            if (Mathf.Approximately(time, 0f) == true)
                return 1f;

            var progress = Mathf.Lerp(0, 1f, currentTime / time);

            return _config.Curve.Evaluate(progress);
        }
    }
}