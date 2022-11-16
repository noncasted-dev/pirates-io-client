using Common.ReadOnlyDictionaries.Editor;
using Global.Services.CurrentSceneHandlers.Logs;
using UnityEditor;

namespace Global.Services.Loggers.Editor.Services.Utils
{
    [CustomPropertyDrawer(typeof(CurrentSceneHandlerLogs))]
    public class CurrentSceneHandlerLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}