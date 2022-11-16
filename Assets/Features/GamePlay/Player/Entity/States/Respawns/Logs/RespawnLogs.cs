#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.States.Respawns.Logs
{
    [Serializable]
    public class RespawnLogs : ReadOnlyDictionary<RespawnLogType, bool>
    {
    }
}