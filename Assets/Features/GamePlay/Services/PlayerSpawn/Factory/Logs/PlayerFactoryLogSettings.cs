#region

using GamePlay.Common.Paths;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Services.PlayerSpawn.Factory.Logs
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.LogsPrefix + "PlayerFactory",
        menuName = GamePlayAssetsPaths.PlayerFactory + "Logs",
        order = 1)]
    public class PlayerFactoryLogSettings : LogSettings<PlayerFactoryLogs, PlayerFactoryLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}