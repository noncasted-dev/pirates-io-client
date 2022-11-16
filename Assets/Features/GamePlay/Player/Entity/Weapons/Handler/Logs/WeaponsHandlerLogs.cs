using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.Weapons.Handler.Logs
{
    [Serializable]
    public class WeaponsHandlerLogs : ReadOnlyDictionary<WeaponsHandlerLogType, bool>
    {
    }
}