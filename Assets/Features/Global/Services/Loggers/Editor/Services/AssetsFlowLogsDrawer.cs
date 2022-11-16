#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.AssetsFlow.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services
{
    [CustomPropertyDrawer(typeof(AssetsFlowLogs))]
    public class AssetsFlowLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}