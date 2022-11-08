using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace Global.Services.ResourcesCleaners.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "ResourcesCleaner",
        menuName = GlobalAssetsPaths.ResourceCleaner + "Logs",
        order = 1)]
    public class ResourcesCleanerLogSettings : LogSettings<ResourcesCleanerLogs, ResourcesCleanerLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}