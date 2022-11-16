using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace Global.Services.FilesFlow.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "FilesFlow",
        menuName = GlobalAssetsPaths.FilesFlow + "Logs", order = 1)]
    public class FilesFlowLogSettings : LogSettings<FilesFlowLogs, FilesFlowLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}