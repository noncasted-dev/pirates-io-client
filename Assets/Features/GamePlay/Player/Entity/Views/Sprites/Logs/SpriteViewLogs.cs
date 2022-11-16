#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.Views.Sprites.Logs
{
    [Serializable]
    public class SpriteViewLogs : ReadOnlyDictionary<SpriteViewLogType, bool>
    {
    }
}