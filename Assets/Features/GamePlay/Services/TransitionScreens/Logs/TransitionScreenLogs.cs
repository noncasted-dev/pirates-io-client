using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Services.TransitionScreens.Logs
{
    [Serializable]
    public class TransitionScreenLogs : ReadOnlyDictionary<TransitionScreenLogType, bool>
    {
    }
}