#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.Network.Connection.Logs
{
    [Serializable]
    public class NetworkConnectorLogs : ReadOnlyDictionary<NetworkConnectorLogType, bool>
    {
    }
}