#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.Views.Transforms.Logs
{
    [Serializable]
    public class TransformLogs : ReadOnlyDictionary<TransformLogType, bool>
    {
    }
}