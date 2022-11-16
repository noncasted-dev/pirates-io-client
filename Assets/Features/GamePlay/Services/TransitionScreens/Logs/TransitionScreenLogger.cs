using Global.Services.Loggers.Runtime;

namespace GamePlay.Services.TransitionScreens.Logs
{
    public class TransitionScreenLogger
    {
        public TransitionScreenLogger(ILogger logger, TransitionScreenLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly TransitionScreenLogSettings _settings;

        public void OnToPlayerDeath()
        {
            if (_settings.IsAvailable(TransitionScreenLogType.ToPlayedDeath) == false)
                return;

            _logger.Log("To player death", _settings.LogParameters);
        }

        public void OnToPlayerSpawn()
        {
            if (_settings.IsAvailable(TransitionScreenLogType.ToPlayerSpawn) == false)
                return;

            _logger.Log("To player spawn", _settings.LogParameters);
        }

        public void OnFadeInStart()
        {
            if (_settings.IsAvailable(TransitionScreenLogType.FadeInStart) == false)
                return;

            _logger.Log("Fade in started", _settings.LogParameters);
        }

        public void OnFadeOutStart()
        {
            if (_settings.IsAvailable(TransitionScreenLogType.FadeOutStart) == false)
                return;

            _logger.Log("Fade out started", _settings.LogParameters);
        }

        public void OnFadeProcess(float progress, float alpha, float speed)
        {
            if (_settings.IsAvailable(TransitionScreenLogType.FadeProcess) == false)
                return;

            _logger.Log($"Fade progress: {progress}, alpha: {alpha}, speed: {speed}", _settings.LogParameters);
        }

        public void OnFadeEnd()
        {
            if (_settings.IsAvailable(TransitionScreenLogType.FadeEnd) == false)
                return;

            _logger.Log("Fade end", _settings.LogParameters);
        }

        public void OnFadeCanceled()
        {
            if (_settings.IsAvailable(TransitionScreenLogType.FadeCanceled) == false)
                return;

            _logger.Log("Fade canceled", _settings.LogParameters);
        }

        public void OnFadeOverlap()
        {
            if (_settings.IsAvailable(TransitionScreenLogType.FadeOverlap) == false)
                return;

            _logger.Error("Fade overlap", _settings.LogParameters);
        }
    }
}