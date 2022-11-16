#region

using Global.Services.FilesFlow.Runtime.Abstract;
using UnityEngine;

#endregion

namespace Global.Services.FilesFlow.Runtime
{
    public class FilesDirectory : IDirectoryProvider
    {
        private readonly string Directory = Application.persistentDataPath + "/Saves/";

        public string GetDirectory()
        {
            System.IO.Directory.CreateDirectory(Directory);

            return Directory;
        }
    }
}