#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.States.Respawns.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor.States
{
    [CustomPropertyDrawer(typeof(RespawnLogs))]
    public class RespawnLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}