using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.Views.Sprites.Logs
{
    [Serializable]
    public class SpriteViewLogs : ReadOnlyDictionary<SpriteViewLogType, bool>
    {
    }
}