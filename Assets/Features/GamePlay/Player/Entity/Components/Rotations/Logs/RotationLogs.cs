#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.Components.Rotations.Logs
{
    [Serializable]
    public class RotationLogs : ReadOnlyDictionary<RotationLogType, bool>
    {
    }
}