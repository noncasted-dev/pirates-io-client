#region

using Common.ReadOnlyDictionaries.Editor;
using Global.GameLoops.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor
{
    [CustomPropertyDrawer(typeof(GameLoopLogs))]
    public class GameLoopLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}