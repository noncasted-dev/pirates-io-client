#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Services.Projectiles.Logs
{
    [Serializable]
    public class ProjectilesLogs : ReadOnlyDictionary<ProjectilesLogType, bool>
    {
    }
}