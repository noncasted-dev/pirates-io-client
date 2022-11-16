using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Handler.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "WeaponsHandler",
        menuName = PlayerAssetsPaths.WeaponsHandler + "Logs")]
    public class WeaponsHandlerLogSettings : LogSettings<WeaponsHandlerLogs, WeaponsHandlerLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}