using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Services.PlayerSpawn.Factory.Logs
{
    [Serializable]
    public class PlayerFactoryLogs : ReadOnlyDictionary<PlayerFactoryLogType, bool>
    {
    }
}