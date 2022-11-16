#region

using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

#endregion

namespace GamePlay.Services.PlayerSpawn.Factory.Logs
{
    public class PlayerFactoryLogger
    {
        public PlayerFactoryLogger(ILogger logger, PlayerFactoryLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly PlayerFactoryLogSettings _settings;

        public void OnInstantiated(Vector2 position)
        {
            if (_settings.IsAvailable(PlayerFactoryLogType.Instantiated) == false)
                return;

            _logger.Log($"Player instantiated at: {position}", _settings.LogParameters);
        }
    }
}