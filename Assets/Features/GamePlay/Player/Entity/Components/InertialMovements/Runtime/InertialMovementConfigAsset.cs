#region

using GamePlay.Player.Entity.Setup.Path;
using NaughtyAttributes;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Components.InertialMovements.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "InertialMovement",
        menuName = PlayerAssetsPaths.InertialMovement + "Config")]
    public class InertialMovementConfigAsset : ScriptableObject
    {
        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _curve;

        [SerializeField] [Min(0f)] private float _lerpSpeed = 1f;
        [SerializeField] [Min(0f)] private float _lerpTime;
        [SerializeField] [Min(0f)] private float _lerpDistanceMultiplier;

        public float LerpSpeed => _lerpSpeed;

        public AnimationCurve Curve => _curve;
        public float LerpTime => _lerpTime;
        public float LerpDistanceMultiplier => _lerpDistanceMultiplier;
    }
}