#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.Network.Session.Leave.Logs
{
    [Serializable]
    public class NetworkSessionLeaveLogs : ReadOnlyDictionary<NetworkSessionLeaveLogType, bool>
    {
    }
}