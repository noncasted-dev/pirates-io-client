using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.AssetsFlow.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "AssetsFlow",
        menuName = GlobalAssetsPaths.AssetsFlow + "Logs", order = 1)]
    public class AssetsFlowLogSettings : LogSettings<AssetsFlowLogs, AssetsFlowLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}