#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.InputViews.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services
{
    [CustomPropertyDrawer(typeof(InputViewLogs))]
    public class InputViewLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}