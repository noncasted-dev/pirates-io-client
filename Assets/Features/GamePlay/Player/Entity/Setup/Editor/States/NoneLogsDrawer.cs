#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.States.None.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor.States
{
    [CustomPropertyDrawer(typeof(NoneLogs))]
    public class NoneLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}