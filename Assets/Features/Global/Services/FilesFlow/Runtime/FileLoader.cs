using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Global.Services.FilesFlow.Logs;
using Global.Services.FilesFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace Global.Services.FilesFlow.Runtime
{
    public class FileLoader : MonoBehaviour, IFileLoader
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

        public bool Load<T>(out T searched) where T : SaveData
        {
            return Load(typeof(T).ToString(), out searched);
        }

        public T LoadOrCreate<T>() where T : SaveData, new()
        {
            if (Load(typeof(T).ToString(), out T searched) == true)
                return searched;

            searched = new T();
            searched.SetName(typeof(T).ToString());
            _logger.OnFileCreated(searched.SaveName);

            return searched;
        }

        public bool Load<T>(string saveName, out T searched) where T : SaveData
        {
            var path = _directoryProvider.GetDirectory() + saveName;
            searched = null;

            if (File.Exists(path) == false)
            {
                _logger.OnFileLoadFailed(saveName, $"No file at path: {path}");
                return false;
            }

            var loaded = Load(path);

            if (loaded is not T casted)
            {
                _logger.OnFileLoadFailed(saveName, $"Wrong type: expected: {typeof(T)}, found: {loaded.GetType()}");
                return false;
            }

            searched = casted;
            _logger.OnFileLoaded(saveName);

            return true;
        }

        private object Load(string path)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            var loaded = _binaryFormatter.Deserialize(stream);
            stream.Close();

            return loaded;
        }
    }
}