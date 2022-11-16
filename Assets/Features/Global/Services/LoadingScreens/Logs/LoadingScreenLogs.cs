using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.LoadingScreens.Logs
{
    [Serializable]
    public class LoadingScreenLogs : ReadOnlyDictionary<LoadingScreenLogType, bool>
    {
    }
}