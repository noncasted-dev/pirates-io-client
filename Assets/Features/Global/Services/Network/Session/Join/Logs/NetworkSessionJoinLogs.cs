using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.Network.Session.Join.Logs
{
    [Serializable]
    public class NetworkSessionJoinLogs : ReadOnlyDictionary<NetworkSessionLogType, bool>
    {
    }
}