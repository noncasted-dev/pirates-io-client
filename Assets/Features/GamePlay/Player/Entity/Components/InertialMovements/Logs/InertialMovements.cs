using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.Components.InertialMovements.Logs
{
    [Serializable]
    public class InertialMovements : ReadOnlyDictionary<InertialMovementType, bool>
    {
    }
}