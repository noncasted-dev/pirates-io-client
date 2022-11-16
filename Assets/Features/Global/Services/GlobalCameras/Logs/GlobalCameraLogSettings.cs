#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.GlobalCameras.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "GlobalCamera",
        menuName = GlobalAssetsPaths.GlobalCamera + "Logs",
        order = 1)]
    public class GlobalCameraLogSettings : LogSettings<GlobalCameraLogs, GlobalCameraLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}