#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.Updaters.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "Updater", menuName = GlobalAssetsPaths.Updater + "Logs",
        order = 1)]
    public class UpdaterLogSettings : LogSettings<UpdaterLogs, UpdaterLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}