#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.States.None.Logs
{
    [Serializable]
    public class NoneLogs : ReadOnlyDictionary<NoneLogType, bool>
    {
    }
}