using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.ScenesFlow.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "ScenesFlow",
        menuName = GlobalAssetsPaths.ScenesFlow + "Logs")]
    public class ScenesFlowLogSettings : LogSettings<ScenesFlowLogs, ScenesFlowLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}