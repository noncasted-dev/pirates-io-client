using Global.Services.Loggers.Runtime;

namespace Global.Services.Updaters.Logs
{
    public class UpdaterLogger
    {
        public UpdaterLogger(ILogger logger, UpdaterLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly UpdaterLogSettings _settings;

        public void OnPreUpdatableAdded(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.PreUpdatableAdd) == false)
                return;

            _logger.Log($"PreUpdatable added, count: {count}", _settings.LogParameters);
        }

        public void OnPreUpdatableRemoved(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.PreUpdatableRemove) == false)
                return;

            _logger.Log($"PreUpdatable removed, count: {count}", _settings.LogParameters);
        }

        public void OnPreUpdateCalled(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.PreUpdateCalled) == false)
                return;

            _logger.Log($"PreUpdate called for {count} listeners", _settings.LogParameters);
        }

        public void OnUpdatableAdded(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.UpdatableAdd) == false)
                return;

            _logger.Log($"Updatable added, count: {count}", _settings.LogParameters);
        }

        public void OnUpdatableRemoved(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.UpdatableRemove) == false)
                return;

            _logger.Log($"Updatable removed, count: {count}", _settings.LogParameters);
        }

        public void OnUpdateCalled(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.UpdateCalled) == false)
                return;

            _logger.Log($"Update called for {count} listeners", _settings.LogParameters);
        }

        public void OnPreFixedUpdatableAdded(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.PreFixedUpdatableAdd) == false)
                return;

            _logger.Log($"PreFixed updatable added, count: {count}", _settings.LogParameters);
        }

        public void OnPreFixedUpdatableRemoved(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.PreFixedUpdatableRemove) == false)
                return;

            _logger.Log($"PreFixed updatable removed, count: {count}", _settings.LogParameters);
        }

        public void OnPreFixedUpdateCalled(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.PreFixedUpdateCalled) == false)
                return;

            _logger.Log($"PreFixed update called for {count} listeners", _settings.LogParameters);
        }

        public void OnFixedUpdatableAdded(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.FixedUpdatableAdd) == false)
                return;

            _logger.Log($"Fixed updatable added, count: {count}", _settings.LogParameters);
        }

        public void OnFixedUpdatableRemoved(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.FixedUpdatableRemove) == false)
                return;

            _logger.Log($"Fixed updatable removed, count: {count}", _settings.LogParameters);
        }

        public void OnFixedUpdateCalled(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.FixedUpdateCalled) == false)
                return;

            _logger.Log($"Fixed update called for {count} listeners", _settings.LogParameters);
        }

        public void OnPostFixedUpdatableAdded(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.PostFixedUpdatableAdd) == false)
                return;

            _logger.Log($"PostFixed updatable added, count: {count}", _settings.LogParameters);
        }

        public void OnPostFixedUpdatableRemoved(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.PostFixedUpdatableRemove) == false)
                return;

            _logger.Log($"PostFixed updatable removed, count: {count}", _settings.LogParameters);
        }

        public void OnPostFixedUpdateCalled(int count)
        {
            if (_settings.IsAvailable(UpdaterLogType.PostFixedUpdateCalled) == false)
                return;

            _logger.Log($"PostFixed update called for {count} listeners", _settings.LogParameters);
        }

        public void OnSpeedModified(float speed)
        {
            if (_settings.IsAvailable(UpdaterLogType.SpeedModified) == false)
                return;

            _logger.Log($"Speed modified: {speed}", _settings.LogParameters);
        }

        public void OnSpeedModifyError(float speed)
        {
            if (_settings.IsAvailable(UpdaterLogType.SpeedModifyError) == false)
                return;

            _logger.Log($"Incorrect speed modification: {speed}", _settings.LogParameters);
        }

        public void OnSpeedModifiableAdded()
        {
            if (_settings.IsAvailable(UpdaterLogType.SpeedModifiableAdd) == false)
                return;

            _logger.Log("Speed modifiable added", _settings.LogParameters);
        }

        public void OnSpeedModifiableRemoved()
        {
            if (_settings.IsAvailable(UpdaterLogType.SpeedModifiableRemove) == false)
                return;

            _logger.Log("Speed modifiable removed", _settings.LogParameters);
        }
    }
}