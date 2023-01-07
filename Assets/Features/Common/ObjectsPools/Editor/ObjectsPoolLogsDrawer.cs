using Common.ObjectsPools.Logs;
using Common.ReadOnlyDictionaries.Editor;
using UnityEditor;

namespace Common.ObjectsPools.Editor
{
    [ReadOnlyDictionaryPriority]
    [CustomPropertyDrawer(typeof(ObjectsPoolLogs))]
    public class ObjectsPoolLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}