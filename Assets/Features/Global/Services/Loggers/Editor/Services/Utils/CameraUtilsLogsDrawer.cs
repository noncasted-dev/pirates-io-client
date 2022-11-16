#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.CameraUtilities.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.Utils
{
    [CustomPropertyDrawer(typeof(CameraUtilsLogs))]
    public class CameraUtilsLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}