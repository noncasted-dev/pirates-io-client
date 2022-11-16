#region

using Global.Common;
using Global.Services.Loggers.Runtime;
using UnityEngine;

#endregion

namespace Global.Services.Profiles.Logs
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.LogsPrefix + "ProfileLog",
        menuName = GlobalAssetsPaths.Profile + "Logs")]
    public class ProfileLogSettings : LogSettings<ProfileLogs, ProfileLogType>
    {
        [SerializeField] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}