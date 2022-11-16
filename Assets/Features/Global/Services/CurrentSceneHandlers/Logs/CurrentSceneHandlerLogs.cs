#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.CurrentSceneHandlers.Logs
{
    [Serializable]
    public class CurrentSceneHandlerLogs : ReadOnlyDictionary<CurrentSceneHandlerLogType, bool>
    {
    }
}