#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.LoadingScreens.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "LoadingScreenLog",
        menuName = GlobalAssetsPaths.LoadingScreen + "Logs",
        order = 1)]
    public class LoadingScreenLogSettings : LogSettings<LoadingScreenLogs, LoadingScreenLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}