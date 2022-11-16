#region

using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.Runs.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "Run",
        menuName = PlayerAssetsPaths.Run + "Logs")]
    public class RunLogSettings : LogSettings<RunLogs, RunLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}