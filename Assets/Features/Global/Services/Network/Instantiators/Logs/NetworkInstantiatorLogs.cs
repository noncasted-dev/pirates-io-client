#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.Network.Instantiators.Logs
{
    [Serializable]
    public class NetworkInstantiatorLogs : ReadOnlyDictionary<NetworkInstantiatorLogType, bool>
    {
    }
}