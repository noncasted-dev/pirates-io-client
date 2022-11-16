#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.States.Floating.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor.Views
{
    [CustomPropertyDrawer(typeof(FloatingStateLogs))]
    public class FloatingStateLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}