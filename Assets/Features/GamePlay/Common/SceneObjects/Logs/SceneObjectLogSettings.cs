#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Common.SceneObjects.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "SceneObject",
        menuName = GlobalAssetsPaths.SceneObjects + "Logs",
        order = 1)]
    public class SceneObjectLogSettings : LogSettings<SceneObjectLogs, SceneObjectLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}