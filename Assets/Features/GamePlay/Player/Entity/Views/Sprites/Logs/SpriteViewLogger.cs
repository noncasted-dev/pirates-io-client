#region

using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

#endregion

namespace GamePlay.Player.Entity.Views.Sprites.Logs
{
    public class SpriteViewLogger
    {
        public SpriteViewLogger(ILogger logger, SpriteViewLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly SpriteViewLogSettings _settings;

        public void OnEnabled()
        {
            if (_settings.IsAvailable(SpriteViewLogType.Enable) == false)
                return;

            _logger.Log("Enabled", _settings.LogParameters);
        }

        public void OnDisabled()
        {
            if (_settings.IsAvailable(SpriteViewLogType.Disable) == false)
                return;

            _logger.Log("Disabled", _settings.LogParameters);
        }

        public void OnMaterialUsed(Material material)
        {
            if (_settings.IsAvailable(SpriteViewLogType.MaterialUse) == false)
                return;

            _logger.Log($"Material {material.name} used", _settings.LogParameters);
        }

        public void OnMaterialSetted(Material material)
        {
            if (_settings.IsAvailable(SpriteViewLogType.MaterialSet) == false)
                return;

            _logger.Log($"Material {material.name} setted", _settings.LogParameters);
        }

        public void OnFlipSetted(bool isFlipped)
        {
            if (_settings.IsAvailable(SpriteViewLogType.FlipSet) == false)
                return;

            _logger.Log($"Flip {isFlipped} setted", _settings.LogParameters);
        }

        public void OnFlippedAlong(Vector2 direction)
        {
            if (_settings.IsAvailable(SpriteViewLogType.FlipSet) == false)
                return;

            _logger.Log($"Flipped along {direction}", _settings.LogParameters);
        }
    }
}