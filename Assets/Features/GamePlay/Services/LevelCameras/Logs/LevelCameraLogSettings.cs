using GamePlay.Common.Paths;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace GamePlay.Services.LevelCameras.Logs
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.LogsPrefix + "LevelCamera",
        menuName = GamePlayAssetsPaths.LevelCamera + "Logs")]
    public class LevelCameraLogSettings : LogSettings<LevelCameraLogs, LevelCameraLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}