using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.CurrentCameras.Logs
{
    [Serializable]
    public class CurrentCameraLogs : ReadOnlyDictionary<CurrentCameraLogType, bool>
    {
    }
}