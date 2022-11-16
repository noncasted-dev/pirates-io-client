#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.Network.Session.Leave.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "NetworkSessionLeave",
        menuName = GlobalAssetsPaths.NetworkSession + "LeaveLogs")]
    public class NetworkSessionLeaveLogSettings : LogSettings<NetworkSessionLeaveLogs, NetworkSessionLeaveLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}