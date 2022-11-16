#region

using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.Floating.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "Floating",
        menuName = PlayerAssetsPaths.Floating + "Logs",
        order = 1)]
    public class FloatingStateLogSettings : LogSettings<FloatingStateLogs, FloatingStateLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}