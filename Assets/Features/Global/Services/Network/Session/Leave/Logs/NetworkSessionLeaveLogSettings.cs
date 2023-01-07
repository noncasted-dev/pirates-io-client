using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Network.Session.Leave.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "NetworkSessionLeave",
        menuName = GlobalAssetsPaths.NetworkSession + "LeaveLogs")]
    public class NetworkSessionLeaveLogSettings : LogSettings<NetworkSessionLeaveLogs, NetworkSessionLeaveLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}