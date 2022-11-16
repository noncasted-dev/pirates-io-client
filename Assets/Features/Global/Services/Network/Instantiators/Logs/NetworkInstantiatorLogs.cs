using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.Network.Instantiators.Logs
{
    [Serializable]
    public class NetworkInstantiatorLogs : ReadOnlyDictionary<NetworkInstantiatorLogType, bool>
    {
    }
}