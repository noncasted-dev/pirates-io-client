using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.UiStateMachines.Logs
{
    [Serializable]
    public class UiStateMachineLogs : ReadOnlyDictionary<UiStateMachineLogType, bool>
    {
    }
}