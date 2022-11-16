using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Views.RigidBodies.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "RigidBody",
        menuName = PlayerAssetsPaths.RigidBodies + "Logs")]
    public class RigidBodyLogSettings : LogSettings<RigidBodyLogs, RigidBodyLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}