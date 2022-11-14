using Common.ReadOnlyDictionaries.Editor;
using UnityEditor;

namespace GamePlay.Player.Entity.Components.Healths.Logs
{
    [CustomPropertyDrawer(typeof(HealthLogs))]
    public class HealthLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}