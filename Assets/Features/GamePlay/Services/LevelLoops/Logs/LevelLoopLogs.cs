using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Services.LevelLoops.Logs
{
    [Serializable]
    public class LevelLoopLogs : ReadOnlyDictionary<LevelLoopLogType, bool>
    {
    }
}