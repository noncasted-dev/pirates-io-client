#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.Updaters.Logs
{
    [Serializable]
    public class UpdaterLogs : ReadOnlyDictionary<UpdaterLogType, bool>
    {
    }
}