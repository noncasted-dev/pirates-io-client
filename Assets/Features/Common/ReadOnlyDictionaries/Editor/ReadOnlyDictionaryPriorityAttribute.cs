using Sirenix.OdinInspector.Editor;

namespace Common.ReadOnlyDictionaries.Editor
{
    public class ReadOnlyDictionaryPriorityAttribute : DrawerPriorityAttribute
    {
        public ReadOnlyDictionaryPriorityAttribute() : base(0, 0, 2)
        {
        }
    }
}