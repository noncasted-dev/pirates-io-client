using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Services.Projectiles.Logs
{
    [Serializable]
    public class ProjectilesLogs : ReadOnlyDictionary<ProjectilesLogType, bool>
    {
    }
}