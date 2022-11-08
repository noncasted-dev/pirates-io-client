using Common.ReadOnlyDictionaries.Editor;
using Global.Services.GlobalCameras.Logs;
using UnityEditor;

namespace Global.Services.Loggers.Editor.Services
{
    [CustomPropertyDrawer(typeof(GlobalCameraLogs))]
    public class GlobalCameraLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}