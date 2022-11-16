using System;

namespace Global.Services.FilesFlow.Runtime.Abstract
{
    [Serializable]
    public abstract class SaveData
    {
        public SaveData(string saveName)
        {
        }

        private string _saveName;

        public string SaveName => _saveName;

        public SaveData SetName(string _name)
        {
            _saveName = _name + "." + GetType().Name;

            return this;
        }
    }
}