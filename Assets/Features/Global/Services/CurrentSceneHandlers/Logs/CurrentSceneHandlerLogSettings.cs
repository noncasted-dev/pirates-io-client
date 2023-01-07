using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.CurrentSceneHandlers.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "CurrentSceneHandler",
        menuName = GlobalAssetsPaths.CurrentSceneHandler + "Logs")]
    public class CurrentSceneHandlerLogSettings : LogSettings<CurrentSceneHandlerLogs, CurrentSceneHandlerLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}