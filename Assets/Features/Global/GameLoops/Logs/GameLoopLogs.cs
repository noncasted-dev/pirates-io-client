#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.GameLoops.Logs
{
    [Serializable]
    public class GameLoopLogs : ReadOnlyDictionary<GameLoopLogType, bool>
    {
    }
}