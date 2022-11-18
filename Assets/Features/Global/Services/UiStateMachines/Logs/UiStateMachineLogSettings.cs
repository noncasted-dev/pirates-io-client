using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace Global.Services.UiStateMachines.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "UiStateMachine",
        menuName = GlobalAssetsPaths.UiStateMachine + "Logs")]
    public class UiStateMachineLogSettings : LogSettings<UiStateMachineLogs, UiStateMachineLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}