using Common.ReadOnlyDictionaries.Editor;
using Global.Services.ItemFactories.Runtime;
using UnityEditor;

namespace Global.Services.ItemFactories.Editor
{
    [CustomPropertyDrawer(typeof(ItemsConfigDictionary))]
    public class ItemsConfigDictionaryDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}