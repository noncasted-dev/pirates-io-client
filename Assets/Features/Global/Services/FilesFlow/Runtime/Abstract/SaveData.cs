using System;

namespace Global.Services.FilesFlow.Runtime.Abstract
{
    [Serializable]
    public abstract class SaveData
    {
        private string _saveName;

        public SaveData(string saveName)
        {
        }

        public string SaveName => _saveName;

        public SaveData SetName(string _name)
        {
            _saveName = _name;

            return this;
        }
    }
}