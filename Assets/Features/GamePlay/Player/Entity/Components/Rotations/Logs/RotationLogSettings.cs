#region

using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Components.Rotations.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "Rotation",
        menuName = PlayerAssetsPaths.Rotation + "Logs",
        order = 1)]
    public class RotationLogSettings : LogSettings<RotationLogs, RotationLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}