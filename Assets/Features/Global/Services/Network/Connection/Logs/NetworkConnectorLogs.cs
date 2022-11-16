using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.Network.Connection.Logs
{
    [Serializable]
    public class NetworkConnectorLogs : ReadOnlyDictionary<NetworkConnectorLogType, bool>
    {
    }
}