using Global.Services.Loggers.Runtime;

namespace Features.Global.Services.Profiles.Logs
{
    public class ProfileLogger
    {
        public ProfileLogger(ILogger logger, ProfileLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly ProfileLogSettings _settings;

        public void OnUserNameSet(string userName)
        {
            if (_settings.IsAvailable(ProfileLogType.UserNameSet) == false)
                return;

            _logger.Log($"User name set: {userName}", _settings.LogParameters);
        }
    }
}