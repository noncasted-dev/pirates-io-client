using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Common.SceneObjects.Logs
{
    [Serializable]
    public class SceneObjectLogs : ReadOnlyDictionary<SceneObjectLogType, bool>
    {
    }
}