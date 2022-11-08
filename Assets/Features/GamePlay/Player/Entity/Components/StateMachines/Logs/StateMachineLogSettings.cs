using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.StateMachines.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "StateMachine",
        menuName = PlayerAssetsPaths.StateMachine + "Logs")]
    public class StateMachineLogSettings : LogSettings<StateMachineLogs, StateMachineLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}