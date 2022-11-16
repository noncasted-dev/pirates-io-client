using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.Components.StateMachines.Logs
{
    [Serializable]
    public class StateMachineLogs : ReadOnlyDictionary<StateMachineLogType, bool>
    {
    }
}