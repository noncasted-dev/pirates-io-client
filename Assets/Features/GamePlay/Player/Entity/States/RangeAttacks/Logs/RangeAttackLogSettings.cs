using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "RangeAttack",
        menuName = PlayerAssetsPaths.RangeAttack + "Logs")]
    public class RangeAttackLogSettings : LogSettings<RangeAttackLogs, RangeAttackLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}