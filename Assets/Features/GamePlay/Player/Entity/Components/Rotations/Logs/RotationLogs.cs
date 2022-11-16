using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.Components.Rotations.Logs
{
    [Serializable]
    public class RotationLogs : ReadOnlyDictionary<RotationLogType, bool>
    {
    }
}