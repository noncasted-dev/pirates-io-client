#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.Updaters.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services
{
    [CustomPropertyDrawer(typeof(UpdaterLogs))]
    public class UpdaterLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}