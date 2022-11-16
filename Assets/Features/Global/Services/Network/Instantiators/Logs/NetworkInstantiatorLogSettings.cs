#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.Network.Instantiators.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "NetworkInstantiatorLog",
        menuName = GlobalAssetsPaths.NetworkInstantiator + "Logs")]
    public class NetworkInstantiatorLogSettings : LogSettings<NetworkInstantiatorLogs, NetworkInstantiatorLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}