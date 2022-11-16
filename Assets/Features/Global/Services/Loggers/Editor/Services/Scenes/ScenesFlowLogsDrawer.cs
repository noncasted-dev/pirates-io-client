#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.ScenesFlow.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.Scenes
{
    [CustomPropertyDrawer(typeof(ScenesFlowLogs))]
    public class ScenesFlowLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}