#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.ResourcesCleaners.Logs
{
    [Serializable]
    public class ResourcesCleanerLogs : ReadOnlyDictionary<ResourcesCleanerLogType, bool>
    {
    }
}