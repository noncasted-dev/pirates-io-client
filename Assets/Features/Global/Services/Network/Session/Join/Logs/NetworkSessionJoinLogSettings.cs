#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.Network.Session.Join.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "NetworkSessionJoin",
        menuName = GlobalAssetsPaths.NetworkSession + "JoinLogs")]
    public class NetworkSessionJoinLogSettings : LogSettings<NetworkSessionJoinLogs, NetworkSessionLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}