using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.CameraUtilities.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "CurrentCameraUtils",
        menuName = GlobalAssetsPaths.CameraUtils + "Logs")]
    public class CameraUtilsLogSettings : LogSettings<CameraUtilsLogs, CameraUtilsLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}