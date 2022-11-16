#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.States.Idles.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor.States
{
    [CustomPropertyDrawer(typeof(IdleLogs))]
    public class IdleLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}