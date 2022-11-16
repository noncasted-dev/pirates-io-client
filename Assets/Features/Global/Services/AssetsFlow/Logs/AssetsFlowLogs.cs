#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.AssetsFlow.Logs
{
    [Serializable]
    public class AssetsFlowLogs : ReadOnlyDictionary<AssetsFlowLogType, bool>
    {
    }
}