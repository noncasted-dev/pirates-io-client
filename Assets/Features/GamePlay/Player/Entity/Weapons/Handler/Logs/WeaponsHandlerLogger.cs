#region

using Global.Services.Loggers.Runtime;

#endregion

namespace GamePlay.Player.Entity.Weapons.Handler.Logs
{
    public class WeaponsHandlerLogger
    {
        public WeaponsHandlerLogger(ILogger logger, WeaponsHandlerLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly WeaponsHandlerLogSettings _settings;

        public void OnInstantiated(string weaponName)
        {
            if (_settings.IsAvailable(WeaponsHandlerLogType.Instantiate) == false)
                return;

            _logger.Log($"Weapon instantiated: {weaponName}", _settings.LogParameters);
        }
    }
}