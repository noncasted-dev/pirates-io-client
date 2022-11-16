using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using Global.Services.FilesFlow.Logs;
using Global.Services.FilesFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace Global.Services.FilesFlow.Runtime
{
    public class FileSaver : MonoBehaviour, IFileSaver
    {
        [Inject]
        private void Construct(IDirectoryProvider directoryProvider, FilesFlowLogger logger)
        {
            _directoryProvider = directoryProvider;
            _logger = logger;
        }

        private readonly BinaryFormatter _binaryFormatter = new();

        private IDirectoryProvider _directoryProvider;
        private FilesFlowLogger _logger;

        public bool Save<T>(T save) where T : SaveData
        {
            try
            {
                WriteInFile(save);
            }
            catch (Exception exception)
            {
                _logger.OnFileSaveFailed(save.SaveName, exception.Message);
                Debugger.Log($"Save failed: {exception.Message}");
                return false;
            }

            _logger.OnFileSaved(save.SaveName);
            SyncWebGL();
            return true;
        }

        [DllImport("__Internal")]
        private static extern void SyncFiles();

        private void WriteInFile<T>(T _data) where T : SaveData
        {
            var path = _directoryProvider.GetDirectory() + _data.SaveName;

            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            _binaryFormatter.Serialize(stream, _data);
            stream.Close();
        }

        private void SyncWebGL()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                SyncFiles();
        }
    }
}