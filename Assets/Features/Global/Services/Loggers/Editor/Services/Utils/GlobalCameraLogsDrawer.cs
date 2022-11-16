#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.GlobalCameras.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.Utils
{
    [CustomPropertyDrawer(typeof(GlobalCameraLogs))]
    public class GlobalCameraLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}