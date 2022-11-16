#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Common.ObjectsPools.Logs
{
    [Serializable]
    public class ObjectsPoolLogs : ReadOnlyDictionary<ObjectsPoolLogType, bool>
    {
    }
}