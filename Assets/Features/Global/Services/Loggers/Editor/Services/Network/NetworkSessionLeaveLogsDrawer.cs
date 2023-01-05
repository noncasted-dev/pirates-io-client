using Common.ReadOnlyDictionaries.Editor;
using Global.Services.Network.Session.Leave.Logs;
using UnityEditor;

namespace Global.Services.Loggers.Editor.Services.Network
{
    [ReadOnlyDictionaryPriority]
    [CustomPropertyDrawer(typeof(NetworkSessionLeaveLogs))]
    public class NetworkSessionLeaveLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}