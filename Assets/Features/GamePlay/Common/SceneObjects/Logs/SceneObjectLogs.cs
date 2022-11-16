#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Common.SceneObjects.Logs
{
    [Serializable]
    public class SceneObjectLogs : ReadOnlyDictionary<SceneObjectLogType, bool>
    {
    }
}