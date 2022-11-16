#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Components.Rotations.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor.Components
{
    [CustomPropertyDrawer(typeof(RotationLogs))]
    public class RotationLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}