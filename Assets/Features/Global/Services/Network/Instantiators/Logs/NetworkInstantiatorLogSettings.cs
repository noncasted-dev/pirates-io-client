using Global.Common;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Network.Instantiators.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "NetworkInstantiatorLog",
        menuName = GlobalAssetsPaths.NetworkInstantiator + "Logs")]
    public class NetworkInstantiatorLogSettings : LogSettings<NetworkInstantiatorLogs, NetworkInstantiatorLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}