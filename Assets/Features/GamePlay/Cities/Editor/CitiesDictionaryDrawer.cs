using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Cities.Global.Registry.Runtime;
using UnityEditor;

namespace GamePlay.Cities.Editor
{
    [CustomPropertyDrawer(typeof(CitiesDictionary))]
    public class CitiesDictionaryDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}