using GamePlay.Common.Paths;
using UnityEngine;

namespace GamePlay.Services.LevelCameras.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "LevelCamera",
        menuName = GamePlayAssetsPaths.LevelCamera + "Config")]
    public class LevelCameraConfigAsset : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float _followSpeed;
        [SerializeField] [Min(0f)] private float _minOverSightDistance;
        [SerializeField] [Min(0f)] private float _maxOverSightDistance;
        [SerializeField] [Min(0f)] private float _xAxisMagnitudeMultiplier;

        public float FollowSpeed => _followSpeed;
        public float MinOverSightDistance => _minOverSightDistance;
        public float MaxOverSightDistance => _maxOverSightDistance;
        public float XAxisMagnitudeMultiplier => _xAxisMagnitudeMultiplier;
    }
}