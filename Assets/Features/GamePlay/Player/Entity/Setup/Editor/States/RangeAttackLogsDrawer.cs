using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using UnityEditor;

namespace GamePlay.Player.Entity.Setup.Editor.States
{
    [CustomPropertyDrawer(typeof(RangeAttackLogs))]
    public class RangeAttackLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}