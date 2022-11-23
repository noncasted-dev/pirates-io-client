using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Components.Definition;
using UnityEditor;

namespace GamePlay.Player.Entity.Components.Editor
{
    [CustomPropertyDrawer(typeof(ShipsDictionary))]
    public class ShipsDictionaryDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}