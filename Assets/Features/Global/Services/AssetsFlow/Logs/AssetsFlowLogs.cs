using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.AssetsFlow.Logs
{
    [Serializable]
    public class AssetsFlowLogs : ReadOnlyDictionary<AssetsFlowLogType, bool>
    {
    }
}