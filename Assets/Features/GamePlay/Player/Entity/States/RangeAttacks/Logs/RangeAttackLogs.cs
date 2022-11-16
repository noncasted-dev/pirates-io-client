#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.States.RangeAttacks.Logs
{
    [Serializable]
    public class RangeAttackLogs : ReadOnlyDictionary<RangeAttackLogType, bool>
    {
    }
}