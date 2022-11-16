#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.CurrentCameras.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.Utils
{
    [CustomPropertyDrawer(typeof(CurrentCameraLogs))]
    public class CurrentCameraLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}