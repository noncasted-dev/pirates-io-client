#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.Components.InertialMovements.Logs
{
    [Serializable]
    public class InertialMovements : ReadOnlyDictionary<InertialMovementType, bool>
    {
    }
}