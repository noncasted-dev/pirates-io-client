using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Services.LevelLoops.Logs;
using UnityEditor;

namespace GamePlay.Services.Editor.Services
{
    [ReadOnlyDictionaryPriority]
    [CustomPropertyDrawer(typeof(LevelLoopLogs))]
    public class LevelLoopLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}