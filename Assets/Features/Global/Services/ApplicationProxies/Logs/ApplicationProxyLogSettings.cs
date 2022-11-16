#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.ApplicationProxies.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "ApplicationProxy",
        menuName = GlobalAssetsPaths.ApplicationProxy + "Logs", order = 1)]
    public class ApplicationProxyLogSettings : LogSettings<ApplicationProxyLogs, ApplicationProxyLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}