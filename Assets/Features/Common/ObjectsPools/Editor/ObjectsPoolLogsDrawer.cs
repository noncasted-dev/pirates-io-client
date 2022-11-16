#region

using Common.ObjectsPools.Logs;
using Common.ReadOnlyDictionaries.Editor;
using UnityEditor;

#endregion

namespace Common.ObjectsPools.Editor
{
    [CustomPropertyDrawer(typeof(ObjectsPoolLogs))]
    public class ObjectsPoolLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}