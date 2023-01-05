using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.LoadingScreens.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "LoadingScreenLog",
        menuName = GlobalAssetsPaths.LoadingScreen + "Logs")]
    public class LoadingScreenLogSettings : LogSettings<LoadingScreenLogs, LoadingScreenLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}