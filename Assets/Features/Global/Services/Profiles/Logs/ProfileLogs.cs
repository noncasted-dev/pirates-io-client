#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.Profiles.Logs
{
    [Serializable]
    public class ProfileLogs : ReadOnlyDictionary<ProfileLogType, bool>
    {
    }
}