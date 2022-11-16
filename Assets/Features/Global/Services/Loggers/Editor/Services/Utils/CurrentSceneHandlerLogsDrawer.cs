#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.CurrentSceneHandlers.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.Utils
{
    [CustomPropertyDrawer(typeof(CurrentSceneHandlerLogs))]
    public class CurrentSceneHandlerLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}