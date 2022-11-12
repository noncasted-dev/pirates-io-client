using Common.ReadOnlyDictionaries.Editor;
using Global.Services.Network.Instantiators.Logs;
using UnityEditor;

namespace Global.Services.Loggers.Editor.Services.Network
{
    [CustomPropertyDrawer(typeof(NetworkInstantiatorLogs))]
    public class NetworkInstantiatorLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}