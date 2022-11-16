using Global.Services.Loggers.Runtime;

namespace Global.Services.FilesFlow.Logs
{
    public class FilesFlowLogger
    {
        public FilesFlowLogger(ILogger logger, FilesFlowLogSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        private readonly ILogger _logger;
        private readonly FilesFlowLogSettings _settings;

        public void OnFileLoaded(string saveName)
        {
            if (_settings.IsAvailable(FilesFlowLogType.FileLoaded) == false)
                return;

            _logger.Log($"File {saveName} Loaded", _settings.LogParameters);
        }

        public void OnFileLoadFailed(string saveName, string exception)
        {
            if (_settings.IsAvailable(FilesFlowLogType.FileLoaded) == false)
                return;

            _logger.Log($"File {saveName} load failed: {exception}", _settings.LogParameters);
        }

        public void OnFileSaved(string saveName)
        {
            if (_settings.IsAvailable(FilesFlowLogType.FileSaved) == false)
                return;

            _logger.Log($"File {saveName} saved", _settings.LogParameters);
        }

        public void OnFileSaveFailed(string saveName, string exception)
        {
            if (_settings.IsAvailable(FilesFlowLogType.FileSaveFailed) == false)
                return;

            _logger.Log($"File {saveName} save failed: {exception}", _settings.LogParameters);
        }

        public void OnFileCreated(string saveName)
        {
            if (_settings.IsAvailable(FilesFlowLogType.FileCreated) == false)
                return;

            _logger.Log($"File {saveName} created", _settings.LogParameters);
        }

        public void OnFileDeleted(string saveName)
        {
            if (_settings.IsAvailable(FilesFlowLogType.FileDeleted) == false)
                return;

            _logger.Log($"File {saveName} deleted", _settings.LogParameters);
        }

        public void OnAllFilesDeleted()
        {
            if (_settings.IsAvailable(FilesFlowLogType.FileDeleted) == false)
                return;

            _logger.Log("All files deleted", _settings.LogParameters);
        }

        public void OnFileDeleteFailed(string saveName, string exception)
        {
            if (_settings.IsAvailable(FilesFlowLogType.FileDeleteFailed) == false)
                return;

            _logger.Error($"File {saveName} delete failed: {exception}", _settings.LogParameters);
        }
    }
}