#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.AssetsFlow.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "AssetsFlow",
        menuName = GlobalAssetsPaths.AssetsFlow + "Logs", order = 1)]
    public class AssetsFlowLogSettings : LogSettings<AssetsFlowLogs, AssetsFlowLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}