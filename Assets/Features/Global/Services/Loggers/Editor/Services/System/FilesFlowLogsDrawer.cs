using Common.ReadOnlyDictionaries.Editor;
using Global.Services.FilesFlow.Logs;
using UnityEditor;

namespace Global.Services.Loggers.Editor.Services.System
{
    [ReadOnlyDictionaryPriority]
    [CustomPropertyDrawer(typeof(FilesFlowLogs))]
    public class FilesFlowLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}