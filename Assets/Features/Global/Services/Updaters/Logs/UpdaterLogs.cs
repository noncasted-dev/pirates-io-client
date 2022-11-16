using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.Updaters.Logs
{
    [Serializable]
    public class UpdaterLogs : ReadOnlyDictionary<UpdaterLogType, bool>
    {
    }
}