using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Views.Pivots.Runtime;
using UnityEditor;

namespace GamePlay.Player.Entity.Setup.Editor.Views
{
    [CustomPropertyDrawer(typeof(PivotsDictionary))]
    public class PivotsDictionaryDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}