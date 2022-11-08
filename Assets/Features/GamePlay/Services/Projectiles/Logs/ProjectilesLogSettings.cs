using GamePlay.Common.Paths;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Logs
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.LogsPrefix + "Projectiles",
        menuName = GamePlayAssetsPaths.Projectiles + "Logs")]
    public class ProjectilesLogSettings : LogSettings<ProjectilesLogs, ProjectilesLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}