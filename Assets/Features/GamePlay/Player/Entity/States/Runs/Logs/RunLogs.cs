using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.States.Runs.Logs
{
    [Serializable]
    public class RunLogs : ReadOnlyDictionary<RunLogType, bool>
    {
    }
}