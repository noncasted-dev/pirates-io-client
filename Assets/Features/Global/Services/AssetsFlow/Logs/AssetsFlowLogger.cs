#region

using Global.Services.Loggers.Runtime;

#endregion

namespace Global.Services.AssetsFlow.Logs
{
    public class AssetsFlowLogger
    {
        public AssetsFlowLogger(ILogger logger, AssetsFlowLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly AssetsFlowLogSettings _settings;

        public void OnLoad(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.Load) == false)
                return;

            _logger.Log($"[Loader]: Asset {assetName} is loaded", _settings.LogParameters);
        }

        public void OnLoadFailed(string assetName, string exception)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.LoadFail) == false)
                return;

            _logger.Log($"[Loader]: Asset {assetName} load failed: {exception}", _settings.LogParameters);
        }

        public void OnStorageAdd(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.StorageAdd) == false)
                return;

            _logger.Log($"[Storage]: Asset {assetName} added to storage", _settings.LogParameters);
        }

        public void OnStorageRemove(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.StorageRemove) == false)
                return;

            _logger.Log($"[Storage]: Asset {assetName} removed from storage", _settings.LogParameters);
        }

        public void OnStorageGetResult(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.StorageGetResult) == false)
                return;

            _logger.Log($"[Storage]: Asset {assetName} taken from storage", _settings.LogParameters);
        }

        public void OnInstantiatorCreated(string typeName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.Preload) == false)
                return;

            _logger.Log($"[InstantiatorFabric]: Instantiator for {typeName} created", _settings.LogParameters);
        }

        public void OnPreloaded(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.Preload) == false)
                return;

            _logger.Log($"[Instantiator]: Asset {assetName} preloaded", _settings.LogParameters);
        }

        public void OnAlreadyPreloaded(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.PreloadFail) == false)
                return;

            _logger.Log($"[Instantiator]: Asset: {assetName} is already loaded", _settings.LogParameters);
        }

        public void OnInstantiate(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.Instantiate) == false)
                return;

            _logger.Log($"[Instantiator]: Asset {assetName} instantiated", _settings.LogParameters);
        }

        public void OnInstantiateFailed(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.InstantiateFail) == false)
                return;

            _logger.Log($"[Instantiator]: Asset {assetName} instantiation failed. No result loaded",
                _settings.LogParameters);
        }

        public void OnRelease(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.Release) == false)
                return;

            _logger.Log($"[Instantiator]: Asset {assetName} released", _settings.LogParameters);
        }

        public void OnUnload(string assetName)
        {
            if (_settings.IsAvailable(AssetsFlowLogType.Unload) == false)
                return;

            _logger.Log($"[Unloader]: Asset {assetName} is unloaded", _settings.LogParameters);
        }
    }
}