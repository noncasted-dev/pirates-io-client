using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Services.TransitionScreens.Logs;
using UnityEditor;

namespace GamePlay.Services.Editor.Services
{
    [ReadOnlyDictionaryPriority]
    [CustomPropertyDrawer(typeof(TransitionScreenLogs))]
    public class TransitionScreenLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}