using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Features.Global.Services.Profiles.Logs
{
    [Serializable]
    public class ProfileLogs : ReadOnlyDictionary<ProfileLogType, bool>
    {
    }
}