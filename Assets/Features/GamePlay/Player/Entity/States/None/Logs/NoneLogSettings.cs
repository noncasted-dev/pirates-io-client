using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.None.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "None",
        menuName = PlayerAssetsPaths.None + "Logs")]
    public class NoneLogSettings : LogSettings<NoneLogs, NoneLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}