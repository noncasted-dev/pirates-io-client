#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.ScenesFlow.Logs
{
    [Serializable]
    public class ScenesFlowLogs : ReadOnlyDictionary<ScenesFlowLogType, bool>
    {
    }
}