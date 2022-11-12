using GamePlay.Player.Entity.Setup.Path;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.InertialMovements.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "InertialMovement",
        menuName = PlayerAssetsPaths.InertialMovement + "Config")]
    public class InertialMovementConfigAsset : ScriptableObject
    {
        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _curve;

        [SerializeField] [Min(0f)] private float _LerpSpeed;
        [SerializeField] [Min(0f)] private float _lerpTime;
        [SerializeField] [Min(0f)] private float _lerpDistanceMultiplier;

        public float LerpSpeed => _LerpSpeed;   
        
        public float Evaluate(float currentTime, Vector2 startDirection, Vector2 targetDirection)
        {
            var distance = Vector2.Distance(startDirection, targetDirection);
            
            var time = distance * _lerpDistanceMultiplier * _lerpTime;

            if (Mathf.Approximately(time, 0f) == true)
                return 1f;
            
            var progress = Mathf.Lerp(0, 1f, currentTime / time);

            return _curve.Evaluate(progress);
        }
    }
}