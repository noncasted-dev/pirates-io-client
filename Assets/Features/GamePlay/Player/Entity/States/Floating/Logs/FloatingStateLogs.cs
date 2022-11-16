#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.States.Floating.Logs
{
    [Serializable]
    public class FloatingStateLogs : ReadOnlyDictionary<FloatingStateLogType, bool>
    {
    }
}