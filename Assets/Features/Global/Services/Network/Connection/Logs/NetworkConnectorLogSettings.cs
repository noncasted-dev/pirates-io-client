using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace Global.Services.Network.Connection.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "NetworkConnector",
        menuName = GlobalAssetsPaths.NetworkConnection + "Logs")]
    public class NetworkConnectorLogSettings : LogSettings<NetworkConnectorLogs, NetworkConnectorLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}