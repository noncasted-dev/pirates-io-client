#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.Views.Animators.Logs
{
    [Serializable]
    public class AnimatorLogs : ReadOnlyDictionary<AnimatorLogType, bool>
    {
    }
}