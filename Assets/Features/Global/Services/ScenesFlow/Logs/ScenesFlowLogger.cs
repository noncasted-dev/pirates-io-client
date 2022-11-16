#region

using Global.Services.Loggers.Runtime;
using UnityEngine.SceneManagement;

#endregion

namespace Global.Services.ScenesFlow.Logs
{
    public class ScenesFlowLogger
    {
        public ScenesFlowLogger(ILogger logger, ScenesFlowLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly ScenesFlowLogSettings _settings;

        public void OnSceneLoad(Scene scene)
        {
            if (_settings.IsAvailable(ScenesFlowLogType.Load) == false)
                return;

            _logger.Log($"Scene {scene.name} is loaded", _settings.LogParameters);
        }

        public void OnSceneUnload(Scene scene)
        {
            if (_settings.IsAvailable(ScenesFlowLogType.Unload) == false)
                return;

            _logger.Log($"Scene {scene.name} is unloaded", _settings.LogParameters);
        }
    }
}