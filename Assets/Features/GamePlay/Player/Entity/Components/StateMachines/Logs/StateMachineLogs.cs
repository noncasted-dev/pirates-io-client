#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.Components.StateMachines.Logs
{
    [Serializable]
    public class StateMachineLogs : ReadOnlyDictionary<StateMachineLogType, bool>
    {
    }
}