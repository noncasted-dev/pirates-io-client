using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace Common.ObjectsPools.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "ObjectsPoolLog",
        menuName = GlobalAssetsPaths.ObjectsPool + "Logs",
        order = 1)]
    public class ObjectsPoolLogSettings : LogSettings<ObjectsPoolLogs, ObjectsPoolLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}