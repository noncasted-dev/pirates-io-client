using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.States.Runs.Logs;
using UnityEditor;

namespace GamePlay.Player.Entity.Setup.Editor.States
{
    [CustomPropertyDrawer(typeof(RunLogs))]
    public class RunLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}