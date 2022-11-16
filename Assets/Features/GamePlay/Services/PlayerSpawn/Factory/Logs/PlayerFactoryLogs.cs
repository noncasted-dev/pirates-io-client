#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Services.PlayerSpawn.Factory.Logs
{
    [Serializable]
    public class PlayerFactoryLogs : ReadOnlyDictionary<PlayerFactoryLogType, bool>
    {
    }
}