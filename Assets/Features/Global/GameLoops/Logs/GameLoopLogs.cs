using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.GameLoops.Logs
{
    [Serializable]
    public class GameLoopLogs : ReadOnlyDictionary<GameLoopLogType, bool>
    {
    }
}