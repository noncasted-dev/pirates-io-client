#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.Network.Session.Join.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.Network
{
    [CustomPropertyDrawer(typeof(NetworkSessionJoinLogs))]
    public class NetworkSessionJoinLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}