using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.States.RangeAttacks.Logs
{
    [Serializable]
    public class RangeAttackLogs : ReadOnlyDictionary<RangeAttackLogType, bool>
    {
    }
}