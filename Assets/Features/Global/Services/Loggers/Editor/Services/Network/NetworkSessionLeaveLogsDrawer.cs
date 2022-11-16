#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.Network.Session.Leave.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.Network
{
    [CustomPropertyDrawer(typeof(NetworkSessionLeaveLogs))]
    public class NetworkSessionLeaveLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}