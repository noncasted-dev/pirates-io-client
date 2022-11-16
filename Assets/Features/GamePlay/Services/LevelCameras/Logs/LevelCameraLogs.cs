#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Services.LevelCameras.Logs
{
    [Serializable]
    public class LevelCameraLogs : ReadOnlyDictionary<LevelCameraLogType, bool>
    {
    }
}