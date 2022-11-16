#region

using UnityEngine;

#endregion

namespace GamePlay.Services.LevelCameras.Runtime
{
    public class LevelCameraConfig : ILevelCameraConfig
    {
        public LevelCameraConfig(LevelCameraConfigAsset asset)
        {
            _asset = asset;
        }

        private readonly LevelCameraConfigAsset _asset;

        public float FollowSpeed => _asset.FollowSpeed;

        public Sight CreateSight(Vector2 direction, float distance)
        {
            var xMultiplier = Mathf.Lerp(1f, _asset.XAxisMagnitudeMultiplier, Mathf.Abs(direction.x));
            distance *= xMultiplier;

            var isOverSight = IsOverSight(distance);

            distance = Clamp(distance);
            distance -= _asset.MinOverSightDistance;

            return new Sight(direction, distance, isOverSight);
        }

        private bool IsOverSight(float distance)
        {
            if (distance > _asset.MinOverSightDistance)
                return true;

            return false;
        }

        private float Clamp(float value)
        {
            return Mathf.Clamp(value, _asset.MinOverSightDistance, _asset.MaxOverSightDistance);
        }
    }
}