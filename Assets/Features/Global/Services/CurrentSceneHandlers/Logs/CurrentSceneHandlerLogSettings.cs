#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.CurrentSceneHandlers.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "CurrentSceneHandler",
        menuName = GlobalAssetsPaths.CurrentSceneHandler + "Logs", order = 1)]
    public class CurrentSceneHandlerLogSettings : LogSettings<CurrentSceneHandlerLogs, CurrentSceneHandlerLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}