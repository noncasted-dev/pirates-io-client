#region

using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.Respawns.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "Respawn",
        menuName = PlayerAssetsPaths.Respawn + "Logs")]
    public class RespawnLogSettings : LogSettings<RespawnLogs, RespawnLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}