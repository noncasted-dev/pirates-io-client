#region

using GamePlay.Common.Paths;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Services.LevelLoops.Logs
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.LogsPrefix + "LevelLoop",
        menuName = GamePlayAssetsPaths.LevelLoop + "Logs",
        order = 1)]
    public class LevelLoopLogSettings : LogSettings<LevelLoopLogs, LevelLoopLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}