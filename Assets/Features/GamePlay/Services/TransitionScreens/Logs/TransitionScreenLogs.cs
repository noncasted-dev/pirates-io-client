#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Services.TransitionScreens.Logs
{
    [Serializable]
    public class TransitionScreenLogs : ReadOnlyDictionary<TransitionScreenLogType, bool>
    {
    }
}