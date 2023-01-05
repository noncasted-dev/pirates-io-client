using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.ResourcesCleaners.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "ResourcesCleaner",
        menuName = GlobalAssetsPaths.ResourceCleaner + "Logs")]
    public class ResourcesCleanerLogSettings : LogSettings<ResourcesCleanerLogs, ResourcesCleanerLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}