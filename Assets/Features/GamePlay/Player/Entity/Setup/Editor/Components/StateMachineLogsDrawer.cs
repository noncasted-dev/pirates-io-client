#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Components.StateMachines.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor.Components
{
    [CustomPropertyDrawer(typeof(StateMachineLogs))]
    public class StateMachineLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}