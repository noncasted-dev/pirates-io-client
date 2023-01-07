using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.CurrentCameras.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "CurrentCamera",
        menuName = GlobalAssetsPaths.CurrentCamera + "Logs")]
    public class CurrentCameraLogSettings : LogSettings<CurrentCameraLogs, CurrentCameraLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}