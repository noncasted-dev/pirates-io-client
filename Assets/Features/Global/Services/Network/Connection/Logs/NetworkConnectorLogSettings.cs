using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Network.Connection.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "NetworkConnector",
        menuName = GlobalAssetsPaths.NetworkConnection + "Logs")]
    public class NetworkConnectorLogSettings : LogSettings<NetworkConnectorLogs, NetworkConnectorLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}