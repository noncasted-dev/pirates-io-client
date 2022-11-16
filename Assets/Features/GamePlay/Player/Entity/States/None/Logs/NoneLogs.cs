using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.States.None.Logs
{
    [Serializable]
    public class NoneLogs : ReadOnlyDictionary<NoneLogType, bool>
    {
    }
}