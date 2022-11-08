using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

namespace Global.Services.InputViews.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "InputView",
        menuName = GlobalAssetsPaths.InputView + "InputView", order = 1)]
    public class InputViewLogSettings : LogSettings<InputViewLogs, InputViewLogType>
    {
        [SerializeField] private LogParameters _parameters;

        public LogParameters LogParameters => _parameters;
    }
}