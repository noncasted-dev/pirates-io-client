using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Views.Sprites.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "SpriteView",
        menuName = PlayerAssetsPaths.Sprites + "Logs")]
    public class SpriteViewLogSettings : LogSettings<SpriteViewLogs, SpriteViewLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}