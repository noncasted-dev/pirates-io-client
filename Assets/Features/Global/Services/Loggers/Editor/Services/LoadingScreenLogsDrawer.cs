using Common.ReadOnlyDictionaries.Editor;
using Global.Services.LoadingScreens.Logs;
using UnityEditor;

namespace Global.Services.Loggers.Editor.Services.UI
{
    [ReadOnlyDictionaryPriority]
    [CustomPropertyDrawer(typeof(LoadingScreenLogs))]
    public class LoadingScreenLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}