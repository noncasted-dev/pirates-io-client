#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Components.InertialMovements.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Player.Entity.Setup.Editor.Components
{
    [CustomPropertyDrawer(typeof(InertialMovements))]
    public class InertialMovementsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}