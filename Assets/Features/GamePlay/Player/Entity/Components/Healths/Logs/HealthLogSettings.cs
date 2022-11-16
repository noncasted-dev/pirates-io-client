#region

using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Components.Healths.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "HealthLog",
        menuName = PlayerAssetsPaths.Health + "Logs")]
    public class HealthLogSettings : LogSettings<HealthLogs, HealthLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}