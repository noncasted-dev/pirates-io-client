#region

using Global.Services.Loggers.Runtime;

#endregion

namespace GamePlay.Services.Projectiles.Logs
{
    public class ProjectilesLogger
    {
        public ProjectilesLogger(ILogger logger, ProjectilesLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly ProjectilesLogSettings _settings;

        public void OnAdd(int count)
        {
            if (_settings.IsAvailable(ProjectilesLogType.Add) == false)
                return;

            _logger.Log($"Added. Count: {count}", _settings.LogParameters);
        }

        public void OnRemove(int count)
        {
            if (_settings.IsAvailable(ProjectilesLogType.Remove) == false)
                return;

            _logger.Log($"Removed. Count: {count}", _settings.LogParameters);
        }

        public void OnUpdate(int count)
        {
            if (_settings.IsAvailable(ProjectilesLogType.Update) == false)
                return;

            _logger.Log($"Updated. Count: {count}", _settings.LogParameters);
        }

        public void OnCollided(string colliderName)
        {
            if (_settings.IsAvailable(ProjectilesLogType.Collision) == false)
                return;

            _logger.Log($"Collided with: {colliderName}", _settings.LogParameters);
        }

        public void OnTriggered(string colliderName)
        {
            if (_settings.IsAvailable(ProjectilesLogType.Trigger) == false)
                return;

            _logger.Log($"Triggered with with: {colliderName}", _settings.LogParameters);
        }

        public void OnWrongTrigger(string colliderName)
        {
            if (_settings.IsAvailable(ProjectilesLogType.WrongTrigger) == false)
                return;

            _logger.Log($"Wrong trigger with: {colliderName}", _settings.LogParameters);
        }
    }
}