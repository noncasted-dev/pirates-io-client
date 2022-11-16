#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.Components.Healths.Logs
{
    [Serializable]
    public class HealthLogs : ReadOnlyDictionary<HealthLogType, bool>
    {
    }
}