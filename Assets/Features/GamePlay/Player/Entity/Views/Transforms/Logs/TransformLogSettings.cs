using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Views.Transforms.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "Transform",
        menuName = PlayerAssetsPaths.Transform + "Logs")]
    public class TransformLogSettings : LogSettings<TransformLogs, TransformLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}