using UnityEngine;

namespace GamePlay.Services.LevelCameras.Runtime
{
    public interface ILevelCamera
    {
        Camera Camera { get; }

        void StartFollow(Transform target);
        void StopFollow();
        void Teleport(Vector2 target);
    }
}