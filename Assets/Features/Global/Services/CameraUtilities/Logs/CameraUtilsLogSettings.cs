using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace Global.Services.CameraUtilities.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "CurrentCameraUtils",
        menuName = GlobalAssetsPaths.CameraUtils + "Logs", order = 1)]
    public class CameraUtilsLogSettings : LogSettings<CameraUtilsLogs, CameraUtilsLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}