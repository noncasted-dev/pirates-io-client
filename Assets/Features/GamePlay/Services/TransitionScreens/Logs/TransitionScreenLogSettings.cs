#region

using GamePlay.Common.Paths;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Services.TransitionScreens.Logs
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.LogsPrefix + "TransitionScreen",
        menuName = GamePlayAssetsPaths.TransitionScreen + "Logs")]
    public class TransitionScreenLogSettings : LogSettings<TransitionScreenLogs, TransitionScreenLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}