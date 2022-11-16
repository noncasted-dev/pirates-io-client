#region

using System;
using System.IO;
using Global.Services.FilesFlow.Logs;
using Global.Services.FilesFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

#endregion

namespace Global.Services.FilesFlow.Runtime
{
    public class FileDeleter : MonoBehaviour, IFileDeleter
    {
        [Inject]
        private void Construct(IDirectoryProvider directoryProvider, FilesFlowLogger logger)
        {
            _directoryProvider = directoryProvider;
            _logger = logger;
        }

        private IDirectoryProvider _directoryProvider;
        private FilesFlowLogger _logger;

        public void Delete<T>() where T : SaveData
        {
            var saveName = typeof(T).ToString();

            Delete(saveName);
        }

        public void Delete(string saveName)
        {
            var path = _directoryProvider.GetDirectory() + saveName;

            if (File.Exists(path) == false)
            {
                _logger.OnFileDeleteFailed(saveName, "No file found");
                return;
            }

            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {
                _logger.OnFileDeleteFailed(saveName, exception.Message);
                throw;
            }

            _logger.OnFileDeleted(saveName);
        }

        public void DeleteAll()
        {
            var names = Directory.GetFiles(_directoryProvider.GetDirectory());

            foreach (var saveName in names)
                Delete(saveName);

            _logger.OnAllFilesDeleted();
        }
    }
}