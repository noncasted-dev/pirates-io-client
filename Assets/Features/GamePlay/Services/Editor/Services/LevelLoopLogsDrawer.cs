#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Services.LevelLoops.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Services.Editor.Services
{
    [CustomPropertyDrawer(typeof(LevelLoopLogs))]
    public class LevelLoopLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}