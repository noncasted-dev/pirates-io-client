#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.States.Idles.Logs
{
    [Serializable]
    public class IdleLogs : ReadOnlyDictionary<IdleLogType, bool>
    {
    }
}