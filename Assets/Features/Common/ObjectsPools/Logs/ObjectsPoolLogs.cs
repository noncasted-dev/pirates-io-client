using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Common.ObjectsPools.Logs
{
    [Serializable]
    public class ObjectsPoolLogs : ReadOnlyDictionary<ObjectsPoolLogType, bool>
    {
    }
}