using Common.ReadOnlyDictionaries.Editor;
using Global.Services.MessageBrokers.Logs;
using UnityEditor;

namespace Global.Services.Loggers.Editor.Services.System
{
    [ReadOnlyDictionaryPriority]
    [CustomPropertyDrawer(typeof(MessageBrokerLogs))]
    public class MessageBrokerLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}