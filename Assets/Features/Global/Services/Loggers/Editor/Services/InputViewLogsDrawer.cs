using Common.ReadOnlyDictionaries.Editor;
using Global.Services.InputViews.Logs;
using UnityEditor;

namespace Global.Services.Loggers.Editor.Services
{
    [ReadOnlyDictionaryPriority]
    [CustomPropertyDrawer(typeof(InputViewLogs))]
    public class InputViewLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}