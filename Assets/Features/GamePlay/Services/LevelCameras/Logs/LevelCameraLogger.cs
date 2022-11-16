#region

using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

#endregion

namespace GamePlay.Services.LevelCameras.Logs
{
    public class LevelCameraLogger
    {
        public LevelCameraLogger(ILogger logger, LevelCameraLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly LevelCameraLogSettings _settings;

        public void OnStartFollow(string targetName)
        {
            if (_settings.IsAvailable(LevelCameraLogType.StartFollow) == false)
                return;

            _logger.Log($"Start follow: {targetName}", _settings.LogParameters);
        }

        public void OnStopFollow(string targetName)
        {
            if (_settings.IsAvailable(LevelCameraLogType.StopFollow) == false)
                return;

            _logger.Log($"Stop follow: {targetName}", _settings.LogParameters);
        }

        public void OnStopFollowError()
        {
            if (_settings.IsAvailable(LevelCameraLogType.StopFollowError) == false)
                return;

            _logger.Log("Stop follow error. No target selected", _settings.LogParameters);
        }

        public void OnTeleport(Vector2 target)
        {
            if (_settings.IsAvailable(LevelCameraLogType.Teleport) == false)
                return;

            _logger.Log($"Teleported to: {target}", _settings.LogParameters);
        }
    }
}