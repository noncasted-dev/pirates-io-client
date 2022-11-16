using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace Global.Services.CurrentCameras.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "CurrentCamera",
        menuName = GlobalAssetsPaths.CurrentCamera + "Logs",
        order = 1)]
    public class CurrentCameraLogSettings : LogSettings<CurrentCameraLogs, CurrentCameraLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}