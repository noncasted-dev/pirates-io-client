#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Services.LevelLoops.Logs
{
    [Serializable]
    public class LevelLoopLogs : ReadOnlyDictionary<LevelLoopLogType, bool>
    {
    }
}