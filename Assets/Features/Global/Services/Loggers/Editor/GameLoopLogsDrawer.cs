using Common.ReadOnlyDictionaries.Editor;
using Global.GameLoops.Logs;
using UnityEditor;

namespace Global.Services.Loggers.Editor
{
    [ReadOnlyDictionaryPriority]
    [CustomPropertyDrawer(typeof(GameLoopLogs))]
    public class GameLoopLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}