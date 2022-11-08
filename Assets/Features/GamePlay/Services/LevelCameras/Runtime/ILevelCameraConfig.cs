using UnityEngine;

namespace GamePlay.Services.LevelCameras.Runtime
{
    public interface ILevelCameraConfig
    {
        float FollowSpeed { get; }

        Sight CreateSight(Vector2 direction, float distance);
    }
}