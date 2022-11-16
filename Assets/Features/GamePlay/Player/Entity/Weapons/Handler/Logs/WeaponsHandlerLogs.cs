#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.Weapons.Handler.Logs
{
    [Serializable]
    public class WeaponsHandlerLogs : ReadOnlyDictionary<WeaponsHandlerLogType, bool>
    {
    }
}