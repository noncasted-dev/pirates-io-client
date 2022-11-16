#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.GameLoops.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "GameLoop",
        menuName = GlobalAssetsPaths.GameLoop + "Logs",
        order = 1)]
    public class GameLoopLogSettings : LogSettings<GameLoopLogs, GameLoopLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}