#region

using GamePlay.Player.Entity.Setup.Path;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Components.InertialMovements.Logs
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.LogsPrefix + "InertialMovement",
        menuName = PlayerAssetsPaths.InertialMovement + "Logs")]
    public class InertialMovementLogSettings : LogSettings<InertialMovements, InertialMovementType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}