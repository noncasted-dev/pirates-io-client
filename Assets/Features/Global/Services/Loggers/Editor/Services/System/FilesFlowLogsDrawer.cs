#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.FilesFlow.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.System
{
    [CustomPropertyDrawer(typeof(FilesFlowLogs))]
    public class FilesFlowLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}