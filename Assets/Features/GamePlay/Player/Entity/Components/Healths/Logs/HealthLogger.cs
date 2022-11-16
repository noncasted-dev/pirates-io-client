using Global.Services.Loggers.Runtime;

namespace GamePlay.Player.Entity.Components.Healths.Logs
{
    public class HealthLogger
    {
        public HealthLogger(ILogger logger, HealthLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly HealthLogSettings _settings;

        public void OnRespawned(int amount)
        {
            if (_settings.IsAvailable(HealthLogType.Respawn) == false)
                return;

            _logger.Log($"Respawned: {amount}", _settings.LogParameters);
        }

        public void OnHealValueException(int heal)
        {
            if (_settings.IsAvailable(HealthLogType.HealValueException) == false)
                return;

            _logger.Log($"Wrong heal value: {heal}", _settings.LogParameters);
        }

        public void OnHealed(int heal, int result)
        {
            if (_settings.IsAvailable(HealthLogType.Respawn) == false)
                return;

            _logger.Log($"Healed: heal: {heal}, result: {result}", _settings.LogParameters);
        }

        public void OnDamageValueException(int heal)
        {
            if (_settings.IsAvailable(HealthLogType.DamageValueException) == false)
                return;

            _logger.Log($"Wrong damage value {heal}", _settings.LogParameters);
        }

        public void OnDamaged(int damage, int result)
        {
            if (_settings.IsAvailable(HealthLogType.Respawn) == false)
                return;

            _logger.Log($"Damaged: damage: {damage}, result: {result}", _settings.LogParameters);
        }
    }
}