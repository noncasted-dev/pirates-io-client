#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Components.Healths.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor.Components
{
    [CustomPropertyDrawer(typeof(HealthLogs))]
    public class HealthLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}