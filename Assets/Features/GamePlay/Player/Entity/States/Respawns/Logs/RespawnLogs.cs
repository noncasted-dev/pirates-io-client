using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.States.Respawns.Logs
{
    [Serializable]
    public class RespawnLogs : ReadOnlyDictionary<RespawnLogType, bool>
    {
    }
}