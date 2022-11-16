#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.Network.Connection.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.Network
{
    [CustomPropertyDrawer(typeof(NetworkConnectorLogs))]
    public class NetworkConnectorLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}