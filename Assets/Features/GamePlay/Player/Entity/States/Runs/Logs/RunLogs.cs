#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.States.Runs.Logs
{
    [Serializable]
    public class RunLogs : ReadOnlyDictionary<RunLogType, bool>
    {
    }
}