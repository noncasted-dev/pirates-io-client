using Common.ReadOnlyDictionaries.Editor;
using UnityEditor;

namespace GamePlay.Player.Entity.Components.InertialMovements.Logs
{
    [CustomPropertyDrawer(typeof(InertialMovements))]
    public class InertialMovementsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}